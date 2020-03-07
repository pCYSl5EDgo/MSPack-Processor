// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil.Cil;
using System;

namespace MSPack.Processor.Core.Embed
{
    public static class InstructionConcatHelper
    {
        public static Instruction[] Concat(
            (
                Instruction[],
                Instruction[]
            )
            tuple
        )
        {
            return Concat(
                tuple.Item1,
                tuple.Item2
            );
        }
        public static Instruction[] Concat(
            Instruction[] array0,
            Instruction[] array1)
        {
            var answer = new Instruction[
                +array0.Length
                + array1.Length
            ];
            var sum = 0;
            Array.Copy(array0, 0, answer, sum, array0.Length);
            sum += array0.Length;
            Array.Copy(array1, 0, answer, sum, array1.Length);
            return answer;
        }
        public static Instruction[] Concat(
            (
                Instruction[],
                Instruction[],
                Instruction[]
            )
            tuple
        )
        {
            return Concat(
                tuple.Item1,
                tuple.Item2,
                tuple.Item3
            );
        }
        public static Instruction[] Concat(
            Instruction[] array0,
            Instruction[] array1,
            Instruction[] array2)
        {
            var answer = new Instruction[
                +array0.Length
                + array1.Length
                + array2.Length
            ];
            var sum = 0;
            Array.Copy(array0, 0, answer, sum, array0.Length);
            sum += array0.Length;
            Array.Copy(array1, 0, answer, sum, array1.Length);
            sum += array1.Length;
            Array.Copy(array2, 0, answer, sum, array2.Length);
            return answer;
        }
        public static Instruction[] Concat(
            (
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[]
            )
            tuple
        )
        {
            return Concat(
                tuple.Item1,
                tuple.Item2,
                tuple.Item3,
                tuple.Item4
            );
        }
        public static Instruction[] Concat(
            Instruction[] array0,
            Instruction[] array1,
            Instruction[] array2,
            Instruction[] array3)
        {
            var answer = new Instruction[
                +array0.Length
                + array1.Length
                + array2.Length
                + array3.Length
            ];
            var sum = 0;
            Array.Copy(array0, 0, answer, sum, array0.Length);
            sum += array0.Length;
            Array.Copy(array1, 0, answer, sum, array1.Length);
            sum += array1.Length;
            Array.Copy(array2, 0, answer, sum, array2.Length);
            sum += array2.Length;
            Array.Copy(array3, 0, answer, sum, array3.Length);
            return answer;
        }
        public static Instruction[] Concat(
            (
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[]
            )
            tuple
        )
        {
            return Concat(
                tuple.Item1,
                tuple.Item2,
                tuple.Item3,
                tuple.Item4,
                tuple.Item5
            );
        }
        public static Instruction[] Concat(
            Instruction[] array0,
            Instruction[] array1,
            Instruction[] array2,
            Instruction[] array3,
            Instruction[] array4)
        {
            var answer = new Instruction[
                +array0.Length
                + array1.Length
                + array2.Length
                + array3.Length
                + array4.Length
            ];
            var sum = 0;
            Array.Copy(array0, 0, answer, sum, array0.Length);
            sum += array0.Length;
            Array.Copy(array1, 0, answer, sum, array1.Length);
            sum += array1.Length;
            Array.Copy(array2, 0, answer, sum, array2.Length);
            sum += array2.Length;
            Array.Copy(array3, 0, answer, sum, array3.Length);
            sum += array3.Length;
            Array.Copy(array4, 0, answer, sum, array4.Length);
            return answer;
        }
        public static Instruction[] Concat(
            (
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[]
            )
            tuple
        )
        {
            return Concat(
                tuple.Item1,
                tuple.Item2,
                tuple.Item3,
                tuple.Item4,
                tuple.Item5,
                tuple.Item6
            );
        }
        public static Instruction[] Concat(
            Instruction[] array0,
            Instruction[] array1,
            Instruction[] array2,
            Instruction[] array3,
            Instruction[] array4,
            Instruction[] array5)
        {
            var answer = new Instruction[
                +array0.Length
                + array1.Length
                + array2.Length
                + array3.Length
                + array4.Length
                + array5.Length
            ];
            var sum = 0;
            Array.Copy(array0, 0, answer, sum, array0.Length);
            sum += array0.Length;
            Array.Copy(array1, 0, answer, sum, array1.Length);
            sum += array1.Length;
            Array.Copy(array2, 0, answer, sum, array2.Length);
            sum += array2.Length;
            Array.Copy(array3, 0, answer, sum, array3.Length);
            sum += array3.Length;
            Array.Copy(array4, 0, answer, sum, array4.Length);
            sum += array4.Length;
            Array.Copy(array5, 0, answer, sum, array5.Length);
            return answer;
        }
        public static Instruction[] Concat(
            (
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[],
                Instruction[]
            )
            tuple
        )
        {
            return Concat(
                tuple.Item1,
                tuple.Item2,
                tuple.Item3,
                tuple.Item4,
                tuple.Item5,
                tuple.Item6,
                tuple.Item7
            );
        }
        public static Instruction[] Concat(
            Instruction[] array0,
            Instruction[] array1,
            Instruction[] array2,
            Instruction[] array3,
            Instruction[] array4,
            Instruction[] array5,
            Instruction[] array6)
        {
            var answer = new Instruction[
                +array0.Length
                + array1.Length
                + array2.Length
                + array3.Length
                + array4.Length
                + array5.Length
                + array6.Length
            ];
            var sum = 0;
            Array.Copy(array0, 0, answer, sum, array0.Length);
            sum += array0.Length;
            Array.Copy(array1, 0, answer, sum, array1.Length);
            sum += array1.Length;
            Array.Copy(array2, 0, answer, sum, array2.Length);
            sum += array2.Length;
            Array.Copy(array3, 0, answer, sum, array3.Length);
            sum += array3.Length;
            Array.Copy(array4, 0, answer, sum, array4.Length);
            sum += array4.Length;
            Array.Copy(array5, 0, answer, sum, array5.Length);
            sum += array5.Length;
            Array.Copy(array6, 0, answer, sum, array6.Length);
            return answer;
        }
    }
}