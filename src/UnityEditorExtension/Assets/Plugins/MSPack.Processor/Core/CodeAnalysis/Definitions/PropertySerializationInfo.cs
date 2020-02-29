// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;
using System;
using System.Linq;
using System.Text;

namespace MSPack.Processor.Core.Definitions
{
    public readonly struct PropertySerializationInfo : IMemberSerializeInfo, IComparable<PropertySerializationInfo>, IComparable<IMemberSerializeInfo>
    {
        public readonly PropertyDefinition Definition;
        public readonly uint Index;
        public readonly bool IsIntKey;
        public readonly int IntKey;
        public readonly string StringKey;

        public string FullName => Definition.FullName;

        public bool IsStringKey => !IsIntKey;

        public bool IsReadable { get; }

        public bool IsWritable { get; }

        public bool IsMessagePackPrimitive { get; }

        public bool CanCallGet { get; }

        public bool CanCallSet { get; }
#if CSHARP_8_0_OR_NEWER
        public FieldReference? BackingFieldReference { get; }
#else
        public FieldReference BackingFieldReference { get; }
#endif

        public bool PublicAccessible => BackingFieldReference is null && (!(Definition.GetMethod is null) && Definition.GetMethod.IsPublic);

        public bool IsValueType => Definition.PropertyType.IsValueType;

        public TypeReference MemberTypeReference => Definition.PropertyType;

        public PropertySerializationInfo(PropertyDefinition definition, uint index, int key, bool isReadable, bool isWritable)
        {
            IsIntKey = true;
            Index = index;
            IntKey = key;
            Definition = definition;
            StringKey = string.Empty;
            IsReadable = isReadable;
            IsWritable = isWritable;
            IsMessagePackPrimitive = definition.PropertyType.IsMessagePackPrimitive();

            CanCallGet = IsReadable && (Definition.GetMethod.IsFinal || Definition.DeclaringType.IsSealed);
            CanCallSet = IsWritable && (Definition.SetMethod.IsFinal || Definition.DeclaringType.IsSealed);

            if (key < 0)
            {
                throw new ArgumentOutOfRangeException(definition.FullName, "Key Attribute value should not be less than 0.");
            }

            var backingField = default(FieldReference);
            if (!(definition.GetMethod is null))
            {
                var instructions = definition.GetMethod.Body.Instructions;
                if (instructions.Count == 3 && instructions[0].OpCode.Code == Code.Ldarg_0 && instructions[1].OpCode.Code == Code.Ldfld && instructions[2].OpCode.Code == Code.Ret)
                {
                    backingField = (FieldReference)instructions[1].Operand;
                }
            }

            if (!(definition.SetMethod is null))
            {
                var instructions = definition.SetMethod.Body.Instructions;
                if (instructions.Count == 4 && instructions[0].OpCode.Code == Code.Ldarg_0 && instructions[1].OpCode.Code == Code.Ldarg_1 && instructions[2].OpCode.Code == Code.Stfld && instructions[3].OpCode.Code == Code.Ret)
                {
                    var tmp = (FieldReference)instructions[2].Operand;
                    if (backingField is null)
                    {
                        backingField = tmp;
                    }
                    else
                    {
                        if (backingField.FullName != tmp.FullName)
                        {
                            backingField = default;
                        }
                    }
                }
            }

            BackingFieldReference = backingField;
        }

        public PropertySerializationInfo(PropertyDefinition definition, uint index, string key, bool isReadable, bool isWritable)
        {
            IsIntKey = false;
            IntKey = -1;
            Index = index;
            StringKey = key;
            Definition = definition;
            IsReadable = isReadable;
            IsWritable = isWritable;
            IsMessagePackPrimitive = definition.PropertyType.IsMessagePackPrimitive();

            CanCallGet = IsReadable && (Definition.GetMethod.IsFinal || Definition.DeclaringType.IsSealed);
            CanCallSet = IsWritable && (Definition.SetMethod.IsFinal || Definition.DeclaringType.IsSealed);

            var backingField = default(FieldReference);
            if (!(definition.GetMethod is null))
            {
                var instructions = definition.GetMethod.Body.Instructions;
                if (instructions.Count == 3 && instructions[0].OpCode.Code == Code.Ldarg_0 && instructions[1].OpCode.Code == Code.Ldfld && instructions[2].OpCode.Code == Code.Ret)
                {
                    backingField = (FieldReference)instructions[1].Operand;
                }
            }

            if (!(definition.SetMethod is null))
            {
                var instructions = definition.SetMethod.Body.Instructions;
                if (instructions.Count == 4 && instructions[0].OpCode.Code == Code.Ldarg_0 && instructions[1].OpCode.Code == Code.Ldarg_1 && instructions[2].OpCode.Code == Code.Stfld && instructions[3].OpCode.Code == Code.Ret)
                {
                    var tmp = (FieldReference)instructions[2].Operand;
                    if (backingField is null)
                    {
                        backingField = tmp;
                    }
                    else
                    {
                        if (backingField.FullName != tmp.FullName)
                        {
                            backingField = default;
                        }
                    }
                }
            }

            BackingFieldReference = backingField;
        }

        bool IMemberSerializeInfo.IsIntKey => IsIntKey;

        int IMemberSerializeInfo.IntKey => IntKey;

        string IMemberSerializeInfo.StringKey => StringKey;

        uint IMemberSerializeInfo.Index => Index;

        public static bool TryParse(PropertyDefinition definition, uint index, bool useMapMode, out PropertySerializationInfo info)
        {
            if (definition.HasParameters || definition.CustomAttributes.Any(CustomAttributeHelper.IsIgnoreMemberAttribute))
            {
                goto FAIL;
            }

            var key = definition.CustomAttributes.FirstOrDefault(CustomAttributeHelper.IsKeyAttribute);
            var getter = definition.GetMethod;
            var setter = definition.SetMethod;

            var getterIsNotPublic = getter is null || !getter.IsPublic;
            var setterIsNotPublic = setter is null || !setter.IsPublic;

            if (key is null)
            {
                if (useMapMode)
                {
                    return TryParseInternalMapMode(definition, index, out info, getter, setter);
                }

                if (!getterIsNotPublic || !setterIsNotPublic)
                {
                    throw new MessagePackGeneratorResolveFailedException("all public members must mark KeyAttribute or IgnoreMemberAttribute : " + definition.FullName);
                }

                goto FAIL;
            }

            if (CustomAttributeHelper.IsStringKeyAttribute(key, out var stringKey))
            {
                info = new PropertySerializationInfo(definition, index, stringKey, !(getter is null), !(setter is null));
                return true;
            }

            if (useMapMode)
            {
                return TryParseInternalMapMode(definition, index, out info, key, getter, setter);
            }

            if (CustomAttributeHelper.IsIntKeyAttribute(key, out var intKey))
            {
                info = new PropertySerializationInfo(definition, index, intKey, !(getter is null), !(setter is null));
                return true;
            }

        FAIL:
            info = default;
            return false;
        }

#if CSHARP_8_0_OR_NEWER
        private static bool TryParseInternalMapMode(PropertyDefinition definition, uint index, out PropertySerializationInfo info, MethodDefinition? getter, MethodDefinition? setter)
#else
        private static bool TryParseInternalMapMode(PropertyDefinition definition, uint index, out PropertySerializationInfo info, MethodDefinition getter, MethodDefinition setter)
#endif
        {
            if (getter is null)
            {
                if (setter is null)
                {
                    info = default;
                    return false;
                }

                if (setter.IsPublic)
                {
                    info = new PropertySerializationInfo(definition, index, definition.Name, false, true);
                    return true;
                }

                info = default;
                return false;
            }

            if (setter is null)
            {
                if (getter.IsPublic)
                {
                    info = new PropertySerializationInfo(definition, index, definition.Name, true, false);
                    return true;
                }

                info = default;
                return false;
            }

            info = new PropertySerializationInfo(definition, index, definition.Name, getter.IsPublic, setter.IsPublic);
            return true;
        }
#if CSHARP_8_0_OR_NEWER
        private static bool TryParseInternalMapMode(PropertyDefinition definition, uint index, out PropertySerializationInfo info, CustomAttribute key, MethodDefinition? getter, MethodDefinition? setter)
#else
        private static bool TryParseInternalMapMode(PropertyDefinition definition, uint index, out PropertySerializationInfo info, CustomAttribute key, MethodDefinition getter, MethodDefinition setter)
#endif
        {
            if (CustomAttributeHelper.IsStringKeyAttribute(key, out var stringKey))
            {
                if (getter is null)
                {
                    if (setter is null)
                    {
                        info = default;
                        return false;
                    }

                    info = new PropertySerializationInfo(definition, index, stringKey, false, true);
                    return true;
                }

                if (setter is null)
                {
                    info = new PropertySerializationInfo(definition, index, stringKey, true, false);
                    return true;
                }

                info = new PropertySerializationInfo(definition, index, stringKey, true, true);
                return true;
            }

            info = default;
            return false;
        }

        public int CompareTo(PropertySerializationInfo other)
        {
            if (IsIntKey ^ other.IsIntKey)
            {
                throw new MessagePackGeneratorResolveFailedException("all members key type must be same. type : " + Definition.FullName);
            }

            if (IsIntKey)
            {
                if (IntKey < other.IntKey)
                {
                    return -1;
                }

                if (IntKey > other.IntKey)
                {
                    return 1;
                }

                throw new MessagePackGeneratorResolveFailedException("key is duplicated, all members key must be unique. type : " + Definition.FullName);
            }

            if (Index < other.Index)
            {
                return -1;
            }

            return Index > other.Index ? 1 : 0;
        }

        public int CompareTo(IMemberSerializeInfo other)
        {
            if (IsIntKey ^ other.IsIntKey)
            {
                throw new MessagePackGeneratorResolveFailedException("all members key type must be same. type : " + Definition.FullName);
            }

            if (IsIntKey)
            {
                if (IntKey < other.IntKey)
                {
                    return -1;
                }

                if (IntKey > other.IntKey)
                {
                    return 1;
                }

                throw new MessagePackGeneratorResolveFailedException("key is duplicated, all members key must be unique. type : " + Definition.FullName);
            }

            if (Index < other.Index)
            {
                return -1;
            }

            return Index > other.Index ? 1 : 0;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.Append("Name : ").Append(FullName)
                .Append(" => ").Append(MemberTypeReference.FullName)
                .Append(" || Key : ");

            if (IsIntKey)
            {
                buffer.Append(IntKey);
            }
            else
            {
                buffer.Append(StringKey);
            }

            if (!(BackingFieldReference is null))
            {
                buffer.Append(" || Auto Property Backing Field : ").Append(BackingFieldReference.Name);
            }

            return buffer.ToString();
        }
    }
}
