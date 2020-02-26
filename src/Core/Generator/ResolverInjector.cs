// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using MSPack.Processor.Core.Provider;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MSPack.Processor.Core
{
    public class ResolverInjector
    {
        private const string FormatterCacheName = "FormatterCache`1";
        private const string FormatterFieldName = "formatter";

        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;
        private readonly TypeDefinition resolver;
        private readonly MethodDefinition getFormatter;

        public ResolverInjector(ModuleDefinition module, string name, TypeProvider provider)
        {
            this.module = module;
            this.provider = provider;

            this.resolver = module.GetType(name);
            if (resolver is null)
            {
                throw new MessagePackGeneratorResolveFailedException("Resolver type should be defined in target module. type : " + name);
            }

            var iFormatterResolver = resolver.Interfaces.FirstOrDefault(x => x.InterfaceType.FullName == "MessagePack.IFormatterResolver");
            if (iFormatterResolver is null)
            {
                throw new MessagePackGeneratorResolveFailedException("Resolver type should implement `MessagePack.IFormatterResolver`. type : " + name);
            }

            this.getFormatter = resolver.Methods.First(IsGetFormatter);
        }

        public void Implement(MethodReference getFormatterMethodReference)
        {
            var (formatterCache, formatterField) = GetOrAddFormatterCache(getFormatterMethodReference);
            ImplementGetFormatter(formatterCache, formatterField);
        }

        private static bool IsGetFormatter(MethodDefinition methodDefinition) => methodDefinition.IsPublic && methodDefinition.GenericParameters.Count == 1 && methodDefinition.Name == "GetFormatter" && methodDefinition.Parameters.Count == 0 && methodDefinition.HasThis;

        private void ImplementGetFormatter(TypeReference formatterCache, FieldReference formatterField)
        {
            getFormatter.Body.Instructions.Clear();
            getFormatter.Body.Variables.Clear();
            getFormatter.Body.InitLocals = false;

            var formatterCacheGenericInstanceType = new GenericInstanceType(formatterCache) { GenericArguments = { getFormatter.GenericParameters[0], }, };
            var formatterFieldTypeGenericInstanceType = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(formatterCache.GenericParameters[0]);
            var formatterFieldGenericInstance = new FieldReference(formatterField.Name, formatterFieldTypeGenericInstanceType, formatterCacheGenericInstanceType);
            var processor = getFormatter.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldsfld, formatterFieldGenericInstance));
            processor.Append(Instruction.Create(OpCodes.Ret));
        }

        private (TypeDefinition, FieldDefinition) GetOrAddFormatterCache(MethodReference getFormatterMethodReference)
        {
            static bool IsFormatterCache(TypeDefinition x) => x.IsNestedPrivate && x.IsSealed && x.IsAbstract && x.Name == FormatterCacheName && x.GenericParameters.Count == 1;

            var formatterCache = resolver.NestedTypes.FirstOrDefault(IsFormatterCache) ?? AddFormatterCache();

            static bool IsFormatter(FieldDefinition x)
            {
                return !x.IsPrivate && x.IsInitOnly && x.IsStatic && x.FieldType is GenericInstanceType instanceType && instanceType.ElementType.FullName == "MessagePack.Formatters.IMessagePackFormatter`1";
            }

            var formatter = formatterCache.Fields.FirstOrDefault(IsFormatter) ?? AddFormatterField(formatterCache);

            static bool IsCctor(MethodDefinition x) => x.IsPrivate && x.IsHideBySig && x.IsSpecialName && x.IsRuntimeSpecialName && x.IsStatic && x.Name == ".cctor";

            var cctor = formatterCache.Methods.FirstOrDefault(IsCctor) ?? AddFormatterCacheCctor(formatterCache);

            ModifyCctor(cctor, formatter, getFormatterMethodReference);

            return (formatterCache, formatter);
        }

        /// <summary>
        /// Implement Cctor.
        /// </summary>
        /// <param name="cctor">static constructor.</param>
        /// <param name="formatterStaticField">formatter static field.</param>
        /// <param name="getFormatterMethodReference">RuntimeTypeHandle -> MessagePack.Formatters.</param>
        private void ModifyCctor(MethodDefinition cctor, FieldReference formatterStaticField, MethodReference getFormatterMethodReference)
        {
            cctor.Body.InitLocals = true;
            cctor.Body.Variables.Clear();
            cctor.Body.Variables.Add(new VariableDefinition(provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterNoGeneric));

            cctor.Body.Instructions.Clear();

            var t = cctor.DeclaringType.GenericParameters[0];

            var processor = cctor.Body.GetILProcessor();
            processor.Append(Instruction.Create(OpCodes.Ldtoken, t));
            processor.Append(Instruction.Create(OpCodes.Call, provider.SystemTypeHelper.GetTypeFromHandle));
            processor.Append(Instruction.Create(OpCodes.Call, getFormatterMethodReference));
            processor.Append(Instruction.Create(OpCodes.Stloc_0));
            processor.Append(Instruction.Create(OpCodes.Ldloc_0));

            var returnInstruction = Instruction.Create(OpCodes.Ret);

            processor.Append(Instruction.Create(OpCodes.Brfalse_S, returnInstruction));

            processor.Append(Instruction.Create(OpCodes.Ldloc_0));
            var formatterTypeGeneric = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(cctor.DeclaringType.GenericParameters[0]);
            processor.Append(Instruction.Create(OpCodes.Castclass, formatterTypeGeneric));
            var formatterCacheGeneric = new GenericInstanceType(cctor.DeclaringType)
            {
                GenericArguments =
                {
                    cctor.DeclaringType.GenericParameters[0],
                },
            };
            processor.Append(Instruction.Create(OpCodes.Stsfld, new FieldReference(FormatterFieldName, formatterStaticField.FieldType, formatterCacheGeneric)));

            processor.Append(returnInstruction);
        }

        private MethodDefinition AddFormatterCacheCctor(TypeDefinition formatterCache)
        {
            var cctor = new MethodDefinition(".cctor", MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.Static, module.TypeSystem.Void)
            {
                HasThis = false,
            };
            formatterCache.Methods.Add(cctor);
            return cctor;
        }

        private TypeDefinition AddFormatterCache()
        {
            var typeSystemObject = module.TypeSystem.Object;
            var formatterCache = new TypeDefinition(string.Empty, FormatterCacheName, TypeAttributes.NestedPrivate | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.Abstract | TypeAttributes.Sealed, typeSystemObject);
            var t = new GenericParameter("T", formatterCache);
            formatterCache.GenericParameters.Add(t);
            resolver.NestedTypes.Add(formatterCache);

            return formatterCache;
        }

        private FieldDefinition AddFormatterField(TypeDefinition formatterCache)
        {
            var iMessagePackFormatter = provider.InterfaceMessagePackFormatterHelper.IMessagePackFormatterGeneric(formatterCache.GenericParameters[0]);
            var formatterField = new FieldDefinition(FormatterFieldName, FieldAttributes.Assembly | FieldAttributes.Static | FieldAttributes.InitOnly, iMessagePackFormatter);
            formatterCache.Fields.Add(formatterField);
            return formatterField;
        }
    }
}
