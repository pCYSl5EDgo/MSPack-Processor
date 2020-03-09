// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace MSPack.Processor.Annotation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class MessagePackObjectGenericVariationAttribute : Attribute
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public uint[] NotPrimitiveValueTypeIndices { get; set; }

        public MessagePackObjectGenericVariationAttribute(Type serializeTargetType)
        {
        }
    }
}