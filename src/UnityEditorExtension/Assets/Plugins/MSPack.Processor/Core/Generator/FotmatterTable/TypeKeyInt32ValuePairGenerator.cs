// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MSPack.Processor.Core.Provider;
using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public sealed class TypeKeyInt32ValuePairGenerator : IPairGenerator
    {
        private const string NameSpace = "pCYSl5EDgo.Mit.License";
        private const string TypeName = "TypeKeyInt32ValuePair";
        private const FieldAttributes ThisFieldAttributes = FieldAttributes.Public | FieldAttributes.InitOnly;

        private readonly ModuleDefinition module;
        private readonly SystemValueTypeHelper valueTypeHelper;
        private readonly SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper readOnlyAttributeHelper;
        private readonly SystemTypeHelper typeHelper;

#if CSHARP_8_0_OR_NEWER
        private TypeDefinition? pair;
        private FieldDefinition? key;
        private FieldDefinition? value;
#else
        private TypeDefinition pair;
        private FieldDefinition key;
        private FieldDefinition value;
#endif


        public TypeKeyInt32ValuePairGenerator(ModuleDefinition module, SystemValueTypeHelper valueTypeHelper, SystemRuntimeCompilerServicesIsReadOnlyAttributeHelper readOnlyAttributeHelper, SystemTypeHelper typeHelper)
        {
            this.module = module;
            this.valueTypeHelper = valueTypeHelper;
            this.readOnlyAttributeHelper = readOnlyAttributeHelper;
            this.typeHelper = typeHelper;
        }

        public TypeDefinition Pair
        {
            get
            {
                if (pair == null)
                {
                    pair = module.GetType(NameSpace, TypeName) ?? Add();
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
                    Add();
                }

#if CSHARP_8_0_OR_NEWER
                return key!;
#else
                return key;
#endif
            }
        }

        public FieldDefinition Value
        {
            get
            {
                if (value is null)
                {
                    Add();
                }
#if CSHARP_8_0_OR_NEWER
                return value!;
#else
                return value;
#endif
            }
        }

        private TypeDefinition Add()
        {
            var typeDefinition = new TypeDefinition(
                NameSpace,
                TypeName,
                TypeAttributes.SequentialLayout | TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                valueTypeHelper.ValueType)
            {
                CustomAttributes =
                {
                    new CustomAttribute(readOnlyAttributeHelper.Ctor),
                },
            };

            module.Types.Add(typeDefinition);

            key = new FieldDefinition("Key", ThisFieldAttributes, typeHelper.Type);
            typeDefinition.Fields.Add(key);

            value = new FieldDefinition("Value", ThisFieldAttributes, module.TypeSystem.Int32);
            typeDefinition.Fields.Add(value);

            return typeDefinition;
        }
    }
}
