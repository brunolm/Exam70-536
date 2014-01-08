using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Exam70_536
{
    /// <summary>
    /// The value types consist of two main categories:
    ///
    /// Structs
    /// Enumerations
    ///
    /// Structs fall into these categories:
    /// Numeric types
    ///   Integral types
    ///   Floating-point types
    ///   decimal
    /// bool
    /// User defined structs.
    /// </summary>
    /// <remarks>
    /// Variables that are based on value types directly contain values. Assigning one value type variable
    /// to another copies the contained value. This differs from the assignment of reference type variables,
    /// which copies a reference to the object but not the object itself.
    /// All value types are derived implicitly from the System.ValueType.
    /// Unlike with reference types, you cannot derive a new type from a value type. However, like reference types,
    /// structs can implement interfaces.
    /// Unlike reference types, a value type cannot contain the null value. However, the nullable types feature
    /// does allow for values types to be assigned to null.
    /// Each value type has an implicit default constructor that initializes the default value of that type.
    /// For information about default values of value types, see Default Values Table.
    /// </remarks>
    /// <see cref="http://msdn.microsoft.com/en-us/library/s1ax56ch.aspx"/>
    [TestClass]
    public class BaseTypesTest
    {
        /// <summary>
        /// The byte keyword denotes an integral type that stores values from 0 to 255.
        /// The sbyte keyword indicates an integral type that stores values from -128 to 127.
        /// </summary>
        /// <remarks>
        /// There is a predefined implicit conversion from byte to short, ushort, int, uint, long, ulong, float, double, or decimal.
        /// You cannot implicitly convert non-literal numeric types of larger storage size to byte. For more information
        /// on the storage sizes of integral types, see Integral Types Table (C# Reference).
        /// 
        /// There is a predefined implicit conversion from sbyte to short, int, long, float, double, or decimal.
        /// You cannot implicitly convert nonliteral numeric types of larger storage size to sbyte
        /// (see Integral Types Table (C# Reference) for the storage sizes of integral types). 
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/5bdb6693.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/d86he86x.aspx"/>
        [TestMethod]
        public void ByteTest()
        {
            // 1 byte
            var byteMin = byte.MinValue; // unsigned
            var byteMax = byte.MaxValue;
            var sByteMin = sbyte.MinValue; // signed
            var sByteMax = sbyte.MaxValue;

            Assert.AreEqual(1, sizeof(byte), "sizeof byte");
            Assert.AreEqual(1, sizeof(sbyte), "sizeof sbyte");
            Assert.AreEqual(sizeof(byte), sizeof(sbyte), "Signed and unsigned have same size");

            Assert.AreNotEqual(byteMin, sByteMin, "Unsigned (byte) starts at 0 and does not take negative numbers");
            Assert.AreEqual(sByteMax + -sByteMin, byteMax, "Unsigned takes negative bytes and use to get a higher positive number");

            Assert.AreEqual(0, default(byte), "Default value for byte is 0");
            Assert.AreEqual(0, default(sbyte), "Default value for sbyte is 0");

            Assert.AreEqual(0, byteMin, "Unsigned min's value is 0");
            Assert.AreEqual(255, byteMax, "Unsigned max's value is 255");
            Assert.AreNotEqual(0, sByteMin, "Signed min's value is not 0"); // (-127)
        }

        /// <summary>
        /// The bool keyword is an alias of System.Boolean. It is used to declare variables
        /// to store the Boolean values, true and false.
        /// </summary>
        /// <remarks>
        /// The default value of a bool variable is false. The default value of a bool? variable is null.
        /// In C#, there is no conversion between the bool type and other types.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/c8f5xwh7.aspx"/>
        [TestMethod]
        public void BoolTest()
        {
            // 4 bytes
            var boolF = false;
            var boolT = true;

            Assert.AreEqual(1, sizeof(bool), "sizeof bool");

            Assert.AreNotEqual(boolF, boolT);

            Assert.AreEqual(default(bool), boolF, "Default value for bool is FALSE");
        }

        /// <summary>
        /// The short keyword denotes an integral data type that stores values from -32,768 to 32,767.
        /// The ushort keyword indicates an integral data type that stores values from 0 to 65,535.
        /// </summary>
        /// <remarks>
        /// You cannot implicitly convert nonliteral numeric types of larger storage size to short.
        /// There is a predefined implicit conversion from short to int, uint, long, ulong, float, double, or decimal.
        /// There is a predefined implicit conversion from ushort to int, uint, long, ulong, float, double, or decimal.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/ybs77ex4.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/cbf1574z.aspx"/>
        [TestMethod]
        public void ShortTest()
        {
            // 2 bytes
            var shortMin = short.MinValue;
            var shortMax = Int16.MaxValue;
            var uShortMin = ushort.MinValue;
            var uShortMax = UInt16.MaxValue;

            Assert.AreEqual(2, sizeof(short), "sizeof short");
            Assert.AreEqual(2, sizeof(ushort), "sizeof ushort");
            Assert.AreEqual(sizeof(short), sizeof(ushort), "Signed and unsigned have same size");

            Assert.AreNotEqual(shortMin, uShortMin);
            Assert.AreNotEqual(shortMax, uShortMax);

            Assert.AreEqual(0, default(short), "Default value for short is 0");
            Assert.AreEqual(0, default(ushort), "Default value for ushort is 0");
        }

        /// <summary>
        /// The char keyword is used to declare an instance of the System.Char structure that represents a
        /// Unicode character.
        /// Unicode characters are used to represent most of the written languages throughout the world.
        /// Range: U+0000 to U+ffff
        /// </summary>
        /// <remarks>
        /// Constants of the char type can be written as character literals, hexadecimal escape sequence,
        /// or Unicode representation. You can also cast the integral character codes.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/x9h8tsay.aspx"/>
        [TestMethod]
        public void CharTest()
        {
            // 2 bytes
            var charMin = char.MinValue;
            var charMax = char.MaxValue;

            Assert.AreEqual(2, sizeof(char), "sizeof char");

            Assert.AreEqual('\0', charMin);
            Assert.AreEqual((char)0xffff, charMax);

            Assert.AreEqual('\0', default(char));
        }

        /// <summary>
        /// The int keyword denotes an integral type that stores values from -2,147,483,648 to 2,147,483,647
        /// The uint keyword signifies an integral type that stores values from 0 to 4,294,967,295
        /// </summary>
        /// <remarks>
        /// There is a predefined implicit conversion from int to long, float, double, or decimal.
        /// When you use the suffix U or u, the type of the literal is determined to be either
        /// uint or ulong according to the numeric value of the literal.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/5kzh1b5w.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/x0sksh43.aspx"/>
        [TestMethod]
        public void IntTest()
        {
            // 4 bytes
            var intMin = int.MinValue;
            var intMax = Int32.MaxValue;
            var uIntMin = uint.MinValue;
            var uIntMax = UInt32.MaxValue;

            Assert.AreEqual(4, sizeof(int), "int");
            Assert.AreEqual(4, sizeof(uint), "uint");

            Assert.AreEqual(0u, uIntMin);
            Assert.AreEqual((uint)0xffffffff, uIntMax);
            Assert.AreNotEqual(0, intMin);
            Assert.IsTrue(intMax < uIntMax);
        }

        /// <summary>
        /// The float keyword signifies a simple type that stores 32-bit floating-point values.
        /// Range: -3.4 × 10^38to +3.4 × 10^38
        /// Precision: 7 digits
        /// </summary>
        /// <remarks>
        /// By default, a real numeric literal on the right side of the assignment operator is treated as double.
        /// Therefore, to initialize a float variable, use the suffix f or F
        /// If one of the floating-point types is double, the expression evaluates to double or bool in relational or Boolean expressions.
        /// If there is no double type in the expression, the expression evaluates to float or bool in relational or Boolean expressions.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/b1e65aza.aspx"/>
        [TestMethod]
        public void FloatTest()
        {
            // 4 bytes
            var floatMin = float.MinValue;
            var floatMax = Single.MaxValue;

            Assert.AreEqual(sizeof(float), 4, "sizeof float");

            Assert.AreEqual(0, default(Single));
            Assert.AreNotEqual(0, floatMin);
            Assert.AreNotEqual((float)int.MinValue, floatMin);
            Assert.AreNotEqual((float)int.MaxValue, floatMax);
        }

        /// <summary>
        /// A platform-specific type that is used to represent a pointer or a handle.
        /// </summary>
        /// <remarks>
        /// The IntPtr type is designed to be an integer whose size is platform-specific.
        /// That is, an instance of this type is expected to be 32-bits on 32-bit hardware
        /// and operating systems, and 64-bits on 64-bit hardware and operating systems.
        /// The IntPtr type can be used by languages that support pointers, and as a common
        /// means of referring to data between languages that do and do not support pointers.
        /// IntPtr objects can also be used to hold handles. For example, instances of IntPtr
        /// are used extensively in the System.IO.FileStream class to hold file handles.
        /// The IntPtr type is CLS-compliant, while the UIntPtr type is not. Only the IntPtr type
        /// is used in the common language runtime. The UIntPtr type is provided mostly to maintain
        /// architectural symmetry with the IntPtr type.
        /// This type implements the ISerializable interface.
        /// </remarks>
        [TestMethod]
        public void IntPtrTest()
        {
            // x bytes
            var intPtrZ = IntPtr.Zero;
            var uIntPtrZ = UIntPtr.Zero;

            // IntPtr does not have a defined size. it is defined in an unsafe context
            Assert.IsTrue(Marshal.SizeOf(typeof(IntPtr)) > 0, "sizeof IntPtr");

            Assert.AreNotEqual(0, intPtrZ); // not same type
            Assert.AreNotEqual(0, uIntPtrZ); // not same type

            Assert.AreEqual(default(IntPtr), intPtrZ);
            Assert.AreEqual(default(UIntPtr), uIntPtrZ);
        }

        /// <summary>
        /// The long keyword denotes an integral type that stores values from –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
        /// The ulong keyword denotes an integral type that stores values from 0 to 18,446,744,073,709,551,615
        /// </summary>
        /// <remarks>
        /// When an integer literal has no suffix, its type is the first of these types in which its value can be represented: int, uint, long, ulong.
        /// When you use the suffix L, the type of the literal integer is determined to be either long or ulong according to its size.
        /// There is a predefined implicit conversion from long to float, double, or decimal.
        /// If you use U or u, the type of the literal integer will be either uint or ulong according to its size.
        /// If you use UL, ul, Ul, uL, LU, lu, Lu, or lU, the type of the literal integer will be ulong.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/ctetwysk.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/t98873t4.aspx"/>
        [TestMethod]
        public void LongTest()
        {
            // 8 bytes
            var longMin = long.MinValue;
            var longMax = Int64.MaxValue;
            var uLongMin = ulong.MinValue;
            var uLongMax = UInt64.MaxValue;

            Assert.AreEqual(8, sizeof(long), "sizeof long");
            Assert.AreEqual(8, sizeof(ulong), "sizeof ulong");

            Assert.AreNotEqual(0, longMin);
            Assert.AreNotEqual(0, longMax);

            Assert.AreEqual(0UL, uLongMin);
            Assert.AreNotEqual(0UL, uLongMax);

            Assert.AreEqual(0xffffffffffffffffUL, uLongMax);
        }

        /// <summary>
        /// The double keyword signifies a simple type that stores 64-bit floating-point values.
        /// Range: ±5.0 × 10^−324 to ±1.7 × 10^308
        /// Precision: 15-16 digits
        /// </summary>
        /// <remarks>
        /// By default, a real numeric literal on the right side of the assignment operator is
        /// treated as double. However, if you want an integer number to be treated as double,
        /// use the suffix d or D.
        /// If one of the floating-point types is double, the expression evaluates to double, or bool in relational or Boolean expressions.
        /// If there is no double type in the expression, it evaluates to float, or bool in relational or Boolean expressions.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/678hzkk9.aspx"/>
        [TestMethod]
        public void DoubleTest()
        {
            // 8 bytes
            var doubleMin = double.MinValue;
            var doubleMax = Double.MaxValue;

            Assert.AreEqual(8, sizeof(double), "size double");

            Assert.AreEqual(0d, default(double));

            Assert.AreNotEqual(Convert.ToDouble(long.MinValue), doubleMin);
            Assert.AreNotEqual(Convert.ToDouble(long.MaxValue), doubleMax);
        }

        /// <summary>
        /// The decimal keyword indicates a 128-bit data type. Compared to floating-point types,
        /// the decimal type has more precision and a smaller range, which makes it appropriate
        /// for financial and monetary calculations.
        /// Range: (-7.9 x 10^28 to 7.9 x 10^28) / (10^0 to 28)
        /// Precision: 28-29 significant digits
        /// </summary>
        /// <remarks>
        /// If you want a numeric real literal to be treated as decimal, use the suffix m or M
        /// There is no implicit conversion between floating-point types and the decimal type;
        /// therefore, a cast must be used to convert between these two types.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/364x0z75.aspx"/>
        [TestMethod]
        public void DecimalTest()
        {
            // 16 bytes
            var decimalMin = decimal.MinValue;
            var decimalMax = Decimal.MaxValue;

            Assert.AreEqual(16, sizeof(decimal), "decimal");

            Assert.AreEqual(0M, default(decimal));

            Assert.AreEqual(decimalMin, -decimalMax);
        }

        /// <summary>
        /// Represents an instant in time, typically expressed as a date and time of day.
        /// The DateTime value type represents dates and times with values ranging
        /// from 12:00:00 midnight, January 1, 0001 Anno Domini (Common Era)
        /// through 11:59:59 P.M., December 31, 9999 A.D. (C.E.).
        /// </summary>
        /// <remarks>
        /// Time values are measured in 100-nanosecond units called ticks.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.datetime.aspx"/>
        [TestMethod]
        public void DateAndTimeTest()
        {
            // 8 bytes
            var dateTime = DateTime.Now;
            var timeSpanMin = TimeSpan.MinValue;
            var timeSpanMax = TimeSpan.MaxValue;

            Assert.AreNotEqual(default(DateTime), dateTime);
            Assert.AreNotEqual(default(TimeSpan), timeSpanMin);
            Assert.AreNotEqual(default(TimeSpan), timeSpanMax);
        }

        [TestMethod]
        public void PointTest()
        {
            Point p = new Point();

            Assert.AreEqual(0, p.X);
            Assert.AreEqual(0, p.Y);

            p.X = 1;
            p.Y = 2;

            Assert.AreEqual(1, p.X);
            Assert.AreEqual(2, p.Y);

            p = new Point(10, 15);

            Assert.AreEqual(10, p.X);
            Assert.AreEqual(15, p.Y);
        }

        [TestMethod]
        public void SizeTest()
        {
            Size sz = new Size();

            Assert.AreEqual(0, sz.Width);
            Assert.AreEqual(0, sz.Height);

            sz.Width = 1;
            sz.Height = 2;

            Assert.AreEqual(1, sz.Width);
            Assert.AreEqual(2, sz.Height);

            sz = new Size(10, 15);

            Assert.AreEqual(10, sz.Width);
            Assert.AreEqual(15, sz.Height);
        }

        [TestMethod]
        public void NullableTypesTest()
        {
            Nullable<int> nullInt = null;
            int? nullInt2 = null;

            Assert.AreEqual<Nullable<int>>(null, nullInt);
            Assert.AreEqual<int?>(null, nullInt2);
        }

        [TestMethod]
        public void FuncParamsTest()
        {
            int x = 0;

            ValueParam(x);

            Assert.AreEqual(0, x);

            ValueRefParam(ref x);

            Assert.AreEqual(5, x);

            StringBuilder s = new StringBuilder();

            s.Append("Initial");

            RefParam(s);

            Assert.AreEqual<string>("Modified", s.ToString());
        }

        internal void ValueParam(int x)
        {
            x = x + 5;
        }
        internal void ValueRefParam(ref int x)
        {
            x = x + 5;
        }
        internal void RefParam(StringBuilder s)
        {
            s.Clear();
            s.Append("Modified");
        }
    }
}
