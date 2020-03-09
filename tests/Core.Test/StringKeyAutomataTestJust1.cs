// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CompoundTestClasses;
using MessagePack;
using NUnit.Framework;

namespace Core.Test
{
    [TestFixture]
    public class StringKeyAutomataTestJust1
    {
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length01(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length01(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length01>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length01_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length01_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length01_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length01_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length01_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length02(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length02(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length02>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length02_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length02_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length02_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length02_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length02_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length03(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length03(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length03>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length03_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length03_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length03_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length03_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length03_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length04(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length04(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length04>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length04_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length04_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length04_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length04_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length04_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length05(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length05(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length05>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length05_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length05_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length05_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length05_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length05_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length06(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length06(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length06>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length06_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length06_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length06_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length06_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length06_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length07(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length07(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length07>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length07_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length07_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length07_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length07_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length07_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length08(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length08(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length08>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length08_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length08_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length08_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length08_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length08_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
        [TestCase(default(string))]
        [TestCase("")]
        [TestCase("こんにちは")]
        [TestCase("そうとも言えるし、そうでもないとも言える。")]
        [TestCase("サバになれ！！！！！")]
        public void Just1Length09(string text)
        {
            var value = new StringKeyAutomataTestTypeJust1Length09(text);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust1Length09>(bytes);
            Assert.AreEqual(value.A, other.A);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_01(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_01(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_01_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_01_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_01_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_02(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_02(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_02_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_02_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_02_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_03(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_03(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_03_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_03_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_03_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_04(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_04(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_04_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_04_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_04_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_05(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_05(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_05_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_05_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_05_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_06(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_06(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_06_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_06_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_06_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_07(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_07(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_07_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_07_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_07_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_08(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_08(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_08_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_08_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_08_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string))]
        [TestCase("", "")]
        [TestCase("さようなら", "マタアイマショウ")]
        [TestCase("その理由を説明するためにはまず銀河の状況を理解する必要がある。", "長くなるぞ。")]
        [TestCase("お前は.NETが好きで俺はJVMが好き！なんの違いもありはしないだろうが！", "違うのだ！！")]
        public void Just2Length09_09(string text0, string text1)
        {
            var value = new StringKeyAutomataTestTypeJust2Length09_09(text0, text1);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust2Length09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_01(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_01(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_01>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_02(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_02(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_02>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_03(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_03(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_03>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_04(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_04(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_04>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_05(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_05(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_05>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_06(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_06(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_06>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_07(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_07(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_07>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_08(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_08(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_08>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }

        [TestCase(default(string), default(string), 0)]
        [TestCase("", "", -1)]
        [TestCase("ほろびのびがく", "おどれおどれおどれ", int.MinValue)]
        [TestCase("goodbye", "hello", int.MaxValue)]
        [TestCase("twinkle twinkle", "little star", 114)]
        public void Just3Length09_09_09(string text0, string text1, int number0)
        {
            var value = new StringKeyAutomataTestTypeJust3Length09_09_09(text0, text1, number0);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<StringKeyAutomataTestTypeJust3Length09_09_09>(bytes);
            Assert.AreEqual(value.A, other.A);
            Assert.AreEqual(value.B, other.B);
            Assert.AreEqual(value.C, other.C);
        }
    }
}