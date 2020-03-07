// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using MSPack.Processor.Core.Provider;
using System;

namespace MSPack.Processor.Core.Embed
{
    public readonly struct AutomataOption
    {
        public readonly Instruction FailInstruction;
        public readonly VariableDefinition SpanVariableDefinition;

        public readonly Func<VariableDefinition> UInt32VariableDefinition;
        public readonly Func<VariableDefinition> UInt64VariableDefinition;
        public readonly Func<VariableDefinition> Int32VariableDefinition;

        public readonly SystemReadOnlySpanHelper ReadOnlySpanHelper;

        public AutomataOption(VariableDefinition spanVariableDefinition, Instruction failInstruction, SystemReadOnlySpanHelper readOnlySpanHelper, Func<VariableDefinition> uInt32VariableDefinition, Func<VariableDefinition> uInt64VariableDefinition, Func<VariableDefinition> int32VariableDefinition)
        {
            SpanVariableDefinition = spanVariableDefinition;
            FailInstruction = failInstruction;
            ReadOnlySpanHelper = readOnlySpanHelper;
            UInt32VariableDefinition = uInt32VariableDefinition;
            UInt64VariableDefinition = uInt64VariableDefinition;
            Int32VariableDefinition = int32VariableDefinition;
        }
    }
}