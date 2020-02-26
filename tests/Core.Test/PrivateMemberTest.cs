using MessagePack;
using NUnit.Framework;
using SimpleTestClasses;

namespace Core.Test
{
    [TestFixture]
    public class PrivateMemberTest
    {
        [TestCase(114, 514)]
        [TestCase(1919, 810)]
        [TestCase(-1, int.MinValue)]
        [TestCase(int.MaxValue, 0)]
        [TestCase(375, -64)]
        public void ClassTest(int a, int b)
        {
            var value = new PrivateMemberClass(a, b);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<PrivateMemberClass>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(b, other.PublicB);
        }

        [TestCase("ぬわあああああん疲れたもぉおおおおおおん", 514)]
        [TestCase("😁👼💙💛", 810)]
        [TestCase("а	б	в	г	д	е	ё	ж	з	и	й	к	л	м	н	о	п	р	с	т	у	ф	х	ц	ч	ш	щ	ъ	ы	ь	э	ю	яа   б   в   г   д   е   ё   ж   з   и", byte.MaxValue)]
        [TestCase("twinkle twinkle little star how i wonder what you are", 0)]
        [TestCase("33", 4)]
        [TestCase(null, 0)]
        [TestCase("", 0)]
        public void StructTest(string a, int b)
        {
            var value = new PrivateMemberStruct(a, b);
            var bytes = MessagePackSerializer.Serialize(value);
            var other = MessagePackSerializer.Deserialize<PrivateMemberStruct>(bytes);
            Assert.True(value.Equals(other));
            Assert.AreEqual(b, other.PublicB);
        }

        [Test]
        public void InterfaceTest()
        {
            IB b = new PrivateMemberClass(114, 514);
            var bBytes = MessagePackSerializer.Serialize(b);
            var bOther = MessagePackSerializer.Deserialize<IB>(bBytes);

            Assert.True(b.Equals(bOther));
            Assert.AreEqual(b.PublicB, bOther.PublicB);

            IB c = new PrivateMemberStruct("なんでや！阪神関係ないやろ！", 33 - 4);
            var cBytes = MessagePackSerializer.Serialize(c);
            var cOther = MessagePackSerializer.Deserialize<IB>(cBytes);
            Assert.AreEqual(c.PublicB, cOther.PublicB);

            Assert.False(bOther.Equals(cOther));
            Assert.True(c.Equals(cOther));
        }
    }
}