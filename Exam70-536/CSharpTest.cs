using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Exam70_536
{
    [TestClass]
    public class CSharpTest
    {
        public delegate void OnBeforeUpdate(DateTime dt);
        public event OnBeforeUpdate BeforeUpdate;

        /// <summary>
        /// Test which type runs faster on a loop (multiple access to memory).
        /// On x86 systems Windows can access INT faster.
        /// On x64 systems Windows can access LONG faster.
        /// </summary>
        [TestMethod]
        public void AccessMemoryMultipleTimesTest()
        {
            int tests = 1000000;

            var bytePerf = Performance.Test("Byte", tests, false,
                () =>
                {
                    for (byte i = 0; i < 200; i++)
                    {

                    }
                });
            var shortPerf = Performance.Test("Short", tests, false,
                () =>
                {
                    for (short i = 0; i < 200; i++)
                    {

                    }
                });
            var intPerf = Performance.Test("Int", tests, false,
                () =>
                {
                    for (int i = 0; i < 200; i++)
                    {

                    }
                });
            var longPerf = Performance.Test("Long", tests, false,
                () =>
                {
                    for (long i = 0; i < 200; i++)
                    {

                    }
                });

            var perfList = new List<TimeSpan> { bytePerf, shortPerf, intPerf, longPerf };

            Assert.IsTrue(perfList.Min() == longPerf // x64
                || perfList.Min() == intPerf); // x86
        }

        /// <summary>
        /// Pointers and memory
        /// </summary>
        [TestMethod]
        public void PointerConecptTest()
        {
            String str; // allocate a pointer

            str = new String(new char[] { 'A' }); // allocate heap memory of the size of the class

            str = null; // pointer changes. after a time GC collects all the memory without a pointer


            /*
             * Static vars have the pointer stored on the HEAP
             * If it points to some data it will never be collected by GC
             * 
             *   Pointers  Heap memory
             *  _________   _________
             * |         | |         |
             * |         | |  data   |
             * |         | |  ^      |
             * |_________| |__static_|
             * 
             */

            /*
             * Structs are primitive types, thus them cannot be setted to null
             */


            // a pointer is removed for variables in this scope when the scope ends
            // after some time GC collects their data

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DestructorTest()
        {
            // Classes with destructos are only collected by GC at the second generation
            // There is one generation to finish their tasks at the destructor
            var d1 = new Destructor();
            var d2 = new Destructor();
            var d3 = new Destructor();

            Assert.AreEqual(3, Destructor.Instances);

            d2 = null;
            GC.Collect();

            // Destructors are called after GC second generator
            // Make GC wait until everything is cleared
            // (otherwise it wouldn't be guaranteed that the count would be right)
            GC.WaitForPendingFinalizers();

            Assert.AreEqual(2, Destructor.Instances);

            d1 = d3;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.AreEqual(1, Destructor.Instances);
        }

        public void CastTest()
        {
            int value = 10;
            long longValue = (long)value;
            value = (int)longValue;

            Assert.AreEqual(value, longValue);

            int.TryParse("9999", out value);

            Assert.AreEqual(9999, value);

            longValue = value; // implicit cast

            Assert.AreEqual(value, longValue);
        }

        /// <summary>
        /// Boxing and unboxing enable value types to be treated as objects.
        /// Boxing a value type packages it inside an instance of the Object reference type.
        /// This allows the value type to be stored on the garbage collected heap.
        /// Unboxing extracts the value type from the object.
        /// </summary>
        /// <remarks>
        /// Performance
        /// In relation to simple assignments, boxing and unboxing are computationally expensive processes.
        /// When a value type is boxed, an entirely new object must be allocated and constructed.
        /// To a lesser degree, the cast required for unboxing is also expensive computationally.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/yz2be5wk(v=vs.80).aspx"/>
        [TestMethod]
        public void BoxingUnboxingTest()
        {
            int num = 222;
            object obj = num; // boxing

            object abj = 111;
            int numI = (int)abj; // unboxing

            Assert.AreEqual(222, num);
            Assert.AreEqual(222, obj);
            Assert.IsFalse(Object.ReferenceEquals(num, obj));

            Assert.AreEqual(111, abj);
            Assert.AreEqual(111, numI);
            Assert.IsFalse(Object.ReferenceEquals(numI, abj));
        }

        /// <summary>
        /// Is a data structure that refers to a static method or to a class instance and an instance method of that class.
        /// </summary>
        /// <remarks>
        /// The Delegate class is the base class for delegate types. However, only the system and compilers can derive
        /// explicitly from the Delegate class or from the MulticastDelegate class. It is also not permissible to derive
        /// a new type from a delegate type. The Delegate class is not considered a delegate type; it is a class used to
        /// derive delegate types.
        /// Most languages implement a delegate keyword, and compilers for those languages are able to derive from the
        /// MulticastDelegate class; therefore, users should use the delegate keyword provided by the language.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.delegate.aspx"/>
        [TestMethod]
        public void DelegateAndEventsTest()
        {
            BeforeUpdate += (dt) =>
            {
                Assert.IsTrue(true);
            };

            // call on another assembly
            BeforeUpdate(DateTime.Now);
        }

        /// <summary>
        /// checked and unchecked keywords: throw exception on overflow of nums
        /// </summary>
        [TestMethod]
        public void IntegerOverflow()
        {
            unchecked
            {
                int value = int.MaxValue + 1;
                Assert.IsTrue(true);
            }
            checked
            {
                try
                {
                    int x = int.MaxValue;
                    int value = int.MaxValue + x;
                    Assert.IsTrue(false);
                }
                catch (OverflowException)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        [TestMethod]
        public void ExceptionTest()
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream("file.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.DeleteOnClose);
                throw new IOException();

                // fs.Dispose(); // wouldn't execute
            }
            catch (IOException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {
                fs.Dispose(); // correct
            }

            Assert.IsTrue(!File.Exists("file.txt"));
        }

        [TestMethod]
        public void StringBuilderTest()
        {
            string str = "";
            StringBuilder strBuilder = new StringBuilder();

            int testTimes = 3000;

            var strConcatTime = Performance.Test("String concat", testTimes, false,
                () =>
                {
                    str = str + "concatValue";
                });
            var strBuilderTime = Performance.Test("StringBuilder concat", testTimes, false,
                () =>
                {
                    strBuilder.Append("concatValue");
                });

            Assert.IsTrue(strBuilderTime < strConcatTime);
        }

        [TestMethod]
        public void GenericTypesTest()
        {
            EventHandler<AssemblyLoadEventArgs> ev = null;
            Nullable<int> i = null;

            Assert.AreEqual(null, i);
            Assert.AreEqual(null, ev);
        }
    }

    public class Destructor
    {
        public static int Instances { get; set; }

        public Destructor()
        {
            ++Instances;
        }

        /// <summary>
        /// Runs 'when' object's pointer change reference (null or other)
        /// </summary>
        /// <remarks>
        /// Unknown execution time.
        /// GC will only clean on the second generation
        /// </remarks>
        ~Destructor()
        {
            --Instances;
        }
    }

}
