// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;
using System.Linq;

namespace MSPack.Processor.Core
{
    public ref struct IgnoresAccessChecksToAttributeGenerator
    {
        private readonly SystemObjectHelper objectHelper;
        private readonly ModuleDefinition module;

        public IgnoresAccessChecksToAttributeGenerator(ModuleDefinition module, SystemObjectHelper objectHelper)
        {
            this.objectHelper = objectHelper;
            this.module = module;
        }

        public void EnsureAccess<T>(T scope)
            where T : IMetadataScope
        {
            var name = scope.Name;
            var assembly = module.Assembly;
            if (assembly.CustomAttributes.Any(x => x.AttributeType.FullName == "System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute" && (string)x.ConstructorArguments[0].Value == name))
            {
                return;
            }

            var attr = GetOrAdd();
            var ctor = attr.Methods.Single(x => x.Name == ".ctor");
            var ca = new CustomAttribute(ctor) { ConstructorArguments = { new CustomAttributeArgument(module.TypeSystem.String, name), }, };
            assembly.CustomAttributes.Add(ca);
        }

        public TypeDefinition GetOrAdd()
        {
            var answer = module.GetType("System.Runtime.CompilerServices", "IgnoresAccessChecksToAttribute");

            if (!(answer is null))
            {
                return answer;
            }

            var attributeUsageAttribute = new TypeReference("System", "AttributeUsageAttribute", module, module.TypeSystem.CoreLibrary, false);
            var attributeTargets = new TypeReference("System", "AttributeTargets", module, module.TypeSystem.CoreLibrary, true);
            var attributeUsageAttributeCtor = new MethodReference(".ctor", module.TypeSystem.Void, attributeUsageAttribute)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("validOn", ParameterAttributes.None, attributeTargets),
                },
            };
            answer = new TypeDefinition("System.Runtime.CompilerServices", "IgnoresAccessChecksToAttribute", TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit, new TypeReference("System", "Attribute", module, module.TypeSystem.CoreLibrary, false))
            {
                CustomAttributes =
                {
                    new CustomAttribute(attributeUsageAttributeCtor)
                    {
                        ConstructorArguments =
                        {
                            new CustomAttributeArgument(attributeTargets, 1)
                        },
                        Properties =
                        {
                            new CustomAttributeNamedArgument("AllowMultiple", new CustomAttributeArgument(module.TypeSystem.Boolean, true)),
                        },
                    },
                },
            };

            var backingField = new FieldDefinition("<AssemblyName>k__BackingField", FieldAttributes.Private | FieldAttributes.InitOnly, module.TypeSystem.String);
            answer.Fields.Add(backingField);

            var property = PropertyUtility.GenerateAutoProperty("AssemblyName", module.TypeSystem.String, backingField, true, false);
            PropertyUtility.RegisterProperty(answer, property);

            var ctor = GenerateCtor(backingField);
            answer.Methods.Add(ctor);

            module.Types.Add(answer);

            return answer;
        }

        private MethodDefinition GenerateCtor(FieldDefinition backingField)
        {
            var ctor = ConstructorUtility.GenerateDefaultConstructor(module, objectHelper);
            ctor.Parameters.Add(new ParameterDefinition("assemblyName", ParameterAttributes.None, module.TypeSystem.String));
            var processor = ctor.Body.GetILProcessor();
            var before = ctor.Body.Instructions[2];
            processor.InsertBefore(before, Instruction.Create(OpCodes.Ldarg_0));
            processor.InsertBefore(before, Instruction.Create(OpCodes.Ldarg_1));
            processor.InsertBefore(before, Instruction.Create(OpCodes.Stfld, backingField));

            return ctor;
        }
    }
}
