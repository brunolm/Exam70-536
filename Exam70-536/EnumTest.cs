using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam70_536
{
    [TestClass]
    public class EnumTest
    {
        [TestMethod]
        public void BasicTest()
        {
            Level level = Level.OverNineThousand;

            Assert.AreEqual((int)Level.OverNineThousand, (int)level);
        }

        [TestMethod]
        public void BasicTypeTest()
        {
            Assert.AreEqual((byte)2, (byte)Elements.Ice);
        }

        [TestMethod]
        public void FlagTest()
        {
            DragonType dragonType = DragonType.Fire;

            dragonType |= DragonType.Wind;

            Assert.IsTrue((dragonType & DragonType.Fire) == DragonType.Fire);
            Assert.IsTrue((dragonType & DragonType.Wind) == DragonType.Wind);
            Assert.AreEqual(dragonType, DragonType.Wind | DragonType.Fire);

            dragonType &= ~DragonType.Wind;
            dragonType |= DragonType.Water;
            dragonType |= DragonType.Earth;

            Assert.IsFalse((dragonType & DragonType.Wind) == DragonType.Wind);
        }

        enum Level
        {
            Easy,
            Normal,
            Hard,
            OverNineThousand,
        }

        /// <summary>
        /// Values cannot be higher than byte.MaxValue (compilation error)
        /// </summary>
        enum Elements : byte
        {
            Hybrird,
            Fire,
            Ice,
        }

        /// <summary>
        /// Flags can store multiple enum values
        /// </summary>
        /// <remarks>Values MUST be set. 1, 2, 4, 8...</remarks>
        [Flags]
        enum DragonType
        {
            [System.ComponentModel.Description("火")]
            Fire = 1,

            [System.ComponentModel.Description("水")]
            Water = 2,

            [System.ComponentModel.Description("風")]
            Wind = 4,

            [System.ComponentModel.Description("土")]
            Earth = 8,
        }
    }
}
