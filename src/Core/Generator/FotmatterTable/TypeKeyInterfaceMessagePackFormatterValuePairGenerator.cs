// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using MSPack.Processor.Core.Provider;

namespace MSPack.Processor.Core
{
    public sealed class TypeKeyInterfaceMessagePackFormatterValuePairGenerator : IPairGenerator
    {
        private const string NameSpace = "pCYSl5EDgo.Mit.License";
        private const string TypeName = "TypeKeyInterfaceMessagePackFormatterValuePair";
        private const FieldAttributes ThisFieldAttributes = FieldAttributes.Public | FieldAttributes.InitOnly;

        private readonly ModuleDefinition module;
        private readonly TypeProvider provider;

#if CSHARP_8_0_OR_NEWER
        private TypeDefinition? pair;
        private FieldDefinition? key;
        private FieldDefinition? value;
#else
        private TypeDefinition pair;
        private FieldDefinition key;
        private FieldDefinition value;
#endif

        public TypeKeyInterfaceMessagePackFormatterValuePairGenerator(TypeProvider provider)
        {
            this.provider = provider;
            this.module = provider.Module;
        }

        public TypeDefinition Pair
        {
            get
            {
                if (pair == null)
                {
                    pair = module.GetType(NameSpace, TypeName) ?? Add(out key, out value);
                }

                return pair;
            }
        }

        public FieldDefinition Key
        {
            get
            {
                if (key is null)
                {
                    Add(out key, out value);
                }

                return key;
            }
        }

        public FieldDefinition Value
        {
            get
            {
                if (value is null)
                {
                    Add(out key, out value);
                }

                return value;
            }
        }

        private TypeDefinition Add(out FieldDefinition keyField, out FieldDefinition valueField)
        {
            var type = new TypeDefinition(
                NameSpace,
                TypeName,
                TypeAttributes.SequentialLayout | TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                provider.SystemValueTypeHelper.ValueType)
            {
                CustomAttributes =
                {
                    new CustomAttribute(provider.SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper.Ctor),
                },
            };
            module.Types.Add(type);

            keyField = new FieldDefinition("Key", ThisFieldAttributes, provider.SystemTypeHelper.Type);
            type.Fields.Add(keyField);

            valueField = new FieldDefinition("Value", ThisFieldAttributes, provider.InterfaceMessagePackFormatterHelper.InterfaceMessagePackFormatterNoGeneric);
            type.Fields.Add(valueField);

            return type;
        }
    }
}
