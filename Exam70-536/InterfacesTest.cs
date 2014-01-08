using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Exam70_536
{
    /// <summary>
    /// You should pay attention to:
    /// - All implemented interfaces in this test class,
    /// for what they are used for and how to use them
    /// </summary>
    [TestClass]
    public class InterfacesTest
    {
        [TestMethod]
        public void ComparableTest()
        {
            Comparable<int> comp1 = new Comparable<int>();
            Comparable<int> comp2 = new Comparable<int>();
            Comparable<string> comp3 = new Comparable<string>();

            Assert.AreEqual(0, comp1.CompareTo(0));
            Assert.AreEqual(0, comp1.CompareTo(comp2));

            try
            {
                comp1.CompareTo(comp3);
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }

            comp1.PropertyToCompare = 9305;

            Assert.AreNotEqual(0, comp1.CompareTo(comp2));
        }

        /// <summary>
        /// You should pay attention to:
        /// - When you should use IDispose
        /// </summary>
        [TestMethod]
        public void DisposableTest()
        {
            try
            {
                using (var disp = new Disposable())
                {
                    Assert.AreEqual(0, Disposable.Value);
                } // calls dispose

                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DisposeGenericTypesTest()
        {
            List<Disposable> list = new List<Disposable>();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ConvertibleTest()
        {
            Convertible conv = new Convertible(10, 20);

            Assert.IsTrue(Convert.ToBoolean(conv));
            Assert.AreEqual(22.3606797749979M, Convert.ToDecimal(conv));
        }

        [TestMethod]
        public void CloneableTest()
        {
            Cloneable clone1 = new Cloneable();
            Cloneable clone2 = clone1.Clone() as Cloneable;

            clone2.PowerLevel = 9999;

            Assert.AreNotEqual(clone1.PowerLevel, clone2.PowerLevel);

            Cloneable notAClone = clone1;
            notAClone.PowerLevel = 20000;

            Assert.AreEqual(notAClone.PowerLevel, clone1.PowerLevel);
        }

        [TestMethod]
        public void EquatableTest()
        {
            Equattable<int> eq1 = new Equattable<int>();

            eq1.Value = 10;

            Assert.IsTrue(eq1.Equals(10));
        }

        [TestMethod]
        public void FormattableTest()
        {
            Formattable form = new Formattable();

            form.C = 40;

            Assert.AreEqual("40", form.ToString("", null));
            Assert.AreEqual("104", form.ToString("F", null));
            Assert.AreEqual("313.15", form.ToString("K", null));

            form.C = 100;

            Assert.AreEqual("100", form.ToString("", null));
            Assert.AreEqual("212", form.ToString("F", null));
            Assert.AreEqual("373.15", form.ToString("K", null));
        }

        [TestMethod]
        public void CustomInterfaceTest()
        {
            MasterClass master = new DiscipleClass();
            // DiscipleClass disciple = new MasterClass(); // compilation error

            Assert.AreEqual<int>(1, master.Value);
        }
        interface ICustomInterface
        {
            int Value { get; }
        }
        class MasterClass : ICustomInterface
        {
            public int Value
            {
                get { return 1; }
                private set { throw new NotImplementedException(); }
            }
        }
        class DiscipleClass : MasterClass
        {
        }
    }

    public class Comparable<T> : IComparable<T>, IComparable
    {
        public int PropertyToCompare { get; set; }

        /// <summary>
        /// Compare two elements and return the position
        /// </summary>
        /// <param name="other">Element to compare to</param>
        /// <returns>
        /// Returns 1 with other is null or comes after this.
        /// Returns 0 with elements are equal.
        /// Returns -1 with the current elements comes before.
        /// </returns>
        public int CompareTo(T other)
        {
            if (other == null)
                return 1;

            return this.PropertyToCompare.CompareTo(other);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (Object.ReferenceEquals(this, obj))
                return 0;

            if (!(obj is Comparable<T>))
                throw new ArgumentException("The type is not Comparable<T>");

            return this.PropertyToCompare.CompareTo((obj as Comparable<T>).PropertyToCompare);
        }
    }

    public class Disposable : IDisposable
    {
        public static int Value { get; set; }

        public void Dispose()
        {
            throw new Exception();
        }
    }

    public class Convertible : IConvertible
    {
        double x;
        double y;

        public Convertible(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            if ((x != 0.0) || (y != 0.0))
                return true;
            else
                return false;
        }

        double GetDoubleValue()
        {
            return Math.Sqrt(x * x + y * y);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(GetDoubleValue());
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(GetDoubleValue());
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(GetDoubleValue());
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(GetDoubleValue());
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return GetDoubleValue();
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(GetDoubleValue());
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(GetDoubleValue());
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(GetDoubleValue());
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(GetDoubleValue());
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(GetDoubleValue());
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return String.Format("({0}, {1})", x, y);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(GetDoubleValue(), conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(GetDoubleValue());
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(GetDoubleValue());
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(GetDoubleValue());
        }

    }

    public class Cloneable : ICloneable
    {
        public int PowerLevel { get; set; }

        public Cloneable()
        {
            PowerLevel = 10000;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Equattable<T> : IEquatable<T>
    {
        public T Value { get; set; }

        public bool Equals(T other)
        {
            return this.Value.Equals(other);
        }
    }

    public class Formattable : IFormattable
    {
        public double C { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "F":
                    return ((9 / 5d) * C + 32).ToString();
                case "K":
                    return (C + 273.15).ToString();
                default:
                    return C.ToString();
            }
        }
    }
}
