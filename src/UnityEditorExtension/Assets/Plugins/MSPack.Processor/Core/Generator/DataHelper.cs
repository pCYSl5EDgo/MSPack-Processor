// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Linq;
using Mono.Cecil;

namespace MSPack.Processor.Core
{
    public sealed class DataHelper
    {
        private const string ArrayContainerName = "<PrivateImplementationDetails>";
        private const string StaticArrayName = "__StaticArrayInitTypeSize=";
        private readonly ModuleDefinition module;
        private readonly TypeReference valueType;
#if CSHARP_8_0_OR_NEWER
        private TypeDefinition? dataContainer;
#else
        private TypeDefinition dataContainer;
#endif

        public DataHelper(ModuleDefinition module, TypeReference valueType)
        {
            this.module = module;
            this.valueType = valueType;
            dataContainer = module.GetType(string.Empty, ArrayContainerName);
        }

        private static string NestedName(int length) => StaticArrayName + length.ToString(CultureInfo.InvariantCulture);

        public TypeDefinition DataContainer
        {
            get
            {
                if (dataContainer == null)
                {
                    dataContainer = new TypeDefinition(string.Empty, ArrayContainerName, TypeAttributes.Sealed, module.TypeSystem.Object);
                    module.Types.Add(dataContainer);
                }

                return dataContainer;
            }
        }

#if CSHARP_8_0_OR_NEWER
        public bool TryGetOrAdd(byte[] data, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]out FieldDefinition? field)
#else
        public bool TryGetOrAdd(byte[] data, out FieldDefinition field)
#endif
        {
            if (data.Length == 0)
            {
                field = default;
                return false;
            }

            var type = GetOrAdd(data.Length);
            var container = DataContainer;
            foreach (var fieldDefinition in container.Fields)
            {
                if (!fieldDefinition.IsStatic || !fieldDefinition.IsInitOnly || !string.Equals(fieldDefinition.FieldType.FullName, type.FullName, StringComparison.Ordinal))
                {
                    continue;
                }

                var bytes = fieldDefinition.InitialValue;
                if (data.SequenceEqual(bytes))
                {
                    field = fieldDefinition;
                    return true;
                }
            }

            field = new FieldDefinition(
                "f" + container.Fields.Count.ToString(CultureInfo.InvariantCulture),
                FieldAttributes.Assembly | FieldAttributes.HasFieldRVA | FieldAttributes.InitOnly | FieldAttributes.Static,
                type)
            {
                InitialValue = data,
            };
            container.Fields.Add(field);
            return true;
        }

        private TypeDefinition GetOrAdd(int length)
        {
            var container = DataContainer;
            foreach (var nestedType in container.NestedTypes)
            {
                if (nestedType.IsValueType && nestedType.IsExplicitLayout && nestedType.PackingSize == 1 && nestedType.ClassSize == length)
                {
                    return nestedType;
                }
            }

            var sizeContainer = new TypeDefinition(string.Empty, NestedName(length), TypeAttributes.NestedPrivate | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, valueType)
            {
                ClassSize = length,
                PackingSize = 1,
            };
            container.NestedTypes.Add(sizeContainer);
            return sizeContainer;
        }
    }
}
