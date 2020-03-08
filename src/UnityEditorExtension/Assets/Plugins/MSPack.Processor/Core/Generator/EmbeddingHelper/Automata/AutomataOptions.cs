// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core.Embed
{
    public readonly struct AutomataOption
    {
        public readonly ParameterDefinition ParamSpan;
        public readonly VariableDefinition Span;

        public readonly Func<VariableDefinition> UInt32VariableDefinition;
        public readonly Func<VariableDefinition> UInt64VariableDefinition;
        public readonly Func<VariableDefinition> Int32VariableDefinition;

        public readonly SystemReadOnlySpanHelper ReadOnlySpanHelper;

        public AutomataOption(VariableDefinition span, Func<VariableDefinition> uInt32VariableDefinition, Func<VariableDefinition> uInt64VariableDefinition, Func<VariableDefinition> int32VariableDefinition, ParameterDefinition paramSpan, SystemReadOnlySpanHelper readOnlySpanHelper)
        {
            Span = span;
            UInt32VariableDefinition = uInt32VariableDefinition;
            UInt64VariableDefinition = uInt64VariableDefinition;
            Int32VariableDefinition = int32VariableDefinition;
            ParamSpan = paramSpan;
            ReadOnlySpanHelper = readOnlySpanHelper;
        }
    }
}