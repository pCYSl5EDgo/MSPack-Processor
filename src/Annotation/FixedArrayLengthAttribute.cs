// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace MSPack.Processor.Annotation
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class FixedArrayLengthAttribute : Attribute
    {
        public FixedArrayLengthAttribute(uint length)
        {
        }
    }
}