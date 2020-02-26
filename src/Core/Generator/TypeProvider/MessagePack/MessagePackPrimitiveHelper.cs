// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public static class MessagePackPrimitiveHelper
    {
        public static bool IsMessagePackPrimitive(this TypeReference typeReference)
        {
            switch (typeReference.FullName)
            {
                case "System.Byte":
                case "System.SByte":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Boolean":
                case "System.Char":
                case "System.Single":
                case "System.Double":
                    return true;
                default:
                    return false;
            }
        }
    }
}
