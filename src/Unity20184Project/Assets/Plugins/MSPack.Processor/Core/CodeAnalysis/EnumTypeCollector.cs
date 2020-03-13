// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSPack.Processor.Core.Definitions
{
    public sealed class EnumTypeCollector : IDisposable
    {
        private readonly HashSet<EnumSerializationInfo> enumInfos = new HashSet<EnumSerializationInfo>();

        public EnumSerializationInfo[] Collect(CollectedInfo[] collectedInfos)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < collectedInfos.Length; index++)
            {
                ref readonly var collectedInfo = ref collectedInfos[index];
                Collect(in collectedInfo);
            }

            return enumInfos.ToArray();
        }

        private void Collect(in CollectedInfo collectedInfo)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < collectedInfo.StructSerializationInfos.Length; index++)
            {
                ref readonly var serializationInfo = ref collectedInfo.StructSerializationInfos[index];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < serializationInfo.PropertyInfos.Length; i++)
                {
                    ref readonly var info = ref serializationInfo.PropertyInfos[i];
                    if (HasUnderlyingTypeDeclaration(info, out var underlyingType))
                    {
                        Add(info.MemberTypeReference, underlyingType);
                    }

                    Collect(info);
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < serializationInfo.FieldInfos.Length; i++)
                {
                    ref readonly var info = ref serializationInfo.FieldInfos[i];
                    if (HasUnderlyingTypeDeclaration(info, out var underlyingType))
                    {
                        Add(info.MemberTypeReference, underlyingType);
                    }

                    Collect(info);
                }
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < collectedInfo.ClassSerializationInfos.Length; index++)
            {
                ref readonly var serializationInfo = ref collectedInfo.ClassSerializationInfos[index];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < serializationInfo.PropertyInfos.Length; i++)
                {
                    Collect(serializationInfo.PropertyInfos[i]);
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < serializationInfo.FieldInfos.Length; i++)
                {
                    Collect(serializationInfo.FieldInfos[i]);
                }
            }
        }

        private static bool HasUnderlyingTypeDeclaration<T>(in T info, out EnumUnderlyingType enumUnderlyingType)
            where T : struct, IMemberSerializeInfo
        {
            if (!info.IsValueType || info.CustomAttributes.Count == 0)
            {
                enumUnderlyingType = default;
                return false;
            }

            foreach (var attribute in info.CustomAttributes)
            {
                if (attribute.AttributeType.Name != "EnumUnderlyingTypeDeclarationAttribute" || attribute.ConstructorArguments.Count != 1)
                {
                    continue;
                }

                var kind = (TypeReference)attribute.ConstructorArguments[0].Value;
                if (Enum.TryParse(kind.Name, out enumUnderlyingType))
                {
                    return true;
                }

                throw new MessagePackGeneratorResolveFailedException("Invalid type specification. Type should be one of the enum base type. actual type : " + kind.FullName);
            }

            enumUnderlyingType = default;
            return false;
        }

        private void Collect<T>(in T info)
            where T : struct, IMemberSerializeInfo
        {
            var type = info.MemberTypeReference;
            Collect(type);
        }

        private void Add(TypeReference reference, EnumUnderlyingType underlyingType)
        {
            enumInfos.Add(new EnumSerializationInfo(reference, underlyingType));
        }

        private void Add(TypeDefinition definition)
        {
            enumInfos.Add(new EnumSerializationInfo(definition));
        }

        private void Collect(TypeDefinition definition)
        {
            if (definition.IsEnum)
            {
                Add(definition);
            }
        }

        private void Collect(TypeReference reference)
        {
            while (true)
            {
                switch (reference)
                {
                    case TypeDefinition definition:
                        Collect(definition);
                        return;
                    case ByReferenceType byReference:
                        reference = byReference.ElementType;
                        continue;
                    case PointerType pointer:
                        reference = pointer;
                        continue;
                    case ArrayType array:
                        reference = array.ElementType;
                        continue;
                    case GenericParameter _:
                        return;
                    case GenericInstanceType genericInstanceType:
                        // Generic Type cannot be Enum
                        // Collect(genericInstanceType.ElementType);
                        foreach (var genericArgument in genericInstanceType.GenericArguments)
                        {
                            Collect(genericArgument);
                        }

                        return;
                    default:
                        CollectTypeReference(reference);

                        return;
                }
            }
        }

        private void CollectTypeReference(TypeReference reference)
        {
            if (!reference.IsValueType)
            {
                return;
            }

            try
            {
                Collect(reference.Resolve());
            }
            catch
            {
                // ignored
            }
        }

        public void Dispose() => enumInfos.Clear();
    }
}
