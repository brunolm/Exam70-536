using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam70_536
{
    [TestClass]
    public class StructsTest
    {
        /// <summary>
        /// Do not define a structure unless the type has all of the following characteristics:
        /// 
        /// • It logically represents a single value, similar to primitive types (integer, double, and so on).
        /// • It has an instance size smaller than 16 bytes.
        /// • It is immutable.
        /// • It will not have to be boxed frequently.
        /// 
        /// If one or more of these conditions are not met, create a reference type instead of a structure. Failure to adhere to this guideline can negatively impact performance.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/ms229017.aspx"/>
        /// <see cref="http://stackoverflow.com/q/85553/340760"/>
        public StructsTest()
        {
        }

        [TestMethod]
        public void ShallowStructTest()
        {
            DarkMatterPosition darkMatterPos;

            // Use of possible unassigned variable X
            // var posX = darkMatterPos.X;

            darkMatterPos.X = 10;
            darkMatterPos.Y = -10;
            darkMatterPos.Z = 50;

            Assert.AreEqual(10, darkMatterPos.X);
            Assert.AreEqual(-10, darkMatterPos.Y);
            Assert.AreEqual(50, darkMatterPos.Z);

            DarkMatterPosition darkMatterPosType2 = new DarkMatterPosition();

            Assert.AreEqual(0, darkMatterPosType2.X);
            Assert.AreEqual(0, darkMatterPosType2.Y);
            Assert.AreEqual(0, darkMatterPosType2.Z);
        }

        [TestMethod]
        public void StructWithPropertiesTest()
        {
            SwordStrikePoint sspNoNew;

            // use of unassigned variable X
            // sspNoNew.X = 1;

            SwordStrikePoint ssp = new SwordStrikePoint();

            Assert.AreEqual(0, ssp.X);

            ssp.X = 1;

            Assert.AreEqual(1, ssp.X);
        }

        [TestMethod]
        public void StructWithConstructorTest()
        {
            NinjaPowerLevel ninjaPower = new NinjaPowerLevel(9999M);

            Assert.AreEqual(9999M, ninjaPower.Power);

            // Use of possible unassigned field
            // NinjaPowerLevel ninjaPowerNoInstance;
            // Assert.AreEqual(ninjaPowerNoInstance.Power, 9999M);

            ninjaPower.Power = 99999M;

            Assert.AreEqual(99999M, ninjaPower.Power);
        }
    }

    public struct DarkMatterPosition
    {
        public int X;
        public int Y;
        public int Z;

        public int Explode()
        {
            return X - Y + Z;
        }
    }

    public struct SwordStrikePoint
    {
        public int X { get; set; }
    }

    public struct NinjaPowerLevel
    {
        public decimal Power;

        // Structs cannot contain explicit parameterless constructors
        // public NinjaPowerLevel(){}

        public NinjaPowerLevel(decimal power)
        {
            // Having a constructor on a struct forces to initialize all members
            Power = power;
        }
    }

    /// <summary>
    /// 3D point
    /// </summary>
    /// <see cref="http://stackoverflow.com/a/470416/340760"/>
    public struct ThreeDimensionalPoint
    {
        public readonly int X, Y, Z;
        public ThreeDimensionalPoint(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return String.Format("X={0}, Y={1}, Z={2}", X, Y, Z);
        }

        public override int GetHashCode()
        {
            return (this.X + 2) ^ (this.Y + 2) ^ (this.Z + 2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ThreeDimensionalPoint))
                throw new ArgumentException();
            ThreeDimensionalPoint other = (ThreeDimensionalPoint)obj;
            return this == other;
        }

        public static bool operator ==(ThreeDimensionalPoint p1, ThreeDimensionalPoint p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
        }

        public static bool operator !=(ThreeDimensionalPoint p1, ThreeDimensionalPoint p2)
        {
            return !(p1 == p2);
        }
    }
}
