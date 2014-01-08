using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Exam70_536
{
    [TestClass]
    public class ThreadsTest
    {
        public static object locker = new object();
        private static Mutex mut = new Mutex(); // pay attention to this
        
        [TestMethod]
        public void ThreadTypesTest()
        {
            int value = 0;

            Thread t0 = new Thread(() => { });
            Thread t1 = new Thread((obj) => { });

            t0.Start();
            t1.Start(new { ID = 1, Name = "Goku" });

            t0.Join();
            t1.Join();

            Thread[] threads = new Thread[10];

            for (int f = 0; f < 10; ++f)
            {
                threads[f] = new Thread(() =>
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        /// fails, no locks
                        // Value = Value + 1;
                        // Value++;
                        // ++Value;

                        /// Works
                        /// <see cref="http://stackoverflow.com/q/9338358/340760" />
                        Interlocked.Increment(ref value);

                        //Monitor.Enter(locker);
                        //++value;
                        //Monitor.Exit(locker);

                        //lock (locker)
                        //{
                        //    ++value;
                        //}
                    }
                });
            }
            for (int f = 0; f < 10; f++)
            {
                threads[f].Start();
            }
            for (int f = 0; f < 10; f++)
            {
                threads[f].Join();
            }

            Assert.AreEqual(10000000, value);
        }

        [TestMethod]
        public void ParametizedThreadTest()
        {
            bool result = false;
            Thread t = new Thread(new ParameterizedThreadStart((o) => { result = (bool)o; }));

            t.Start(true);
            t.Join();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ThreadPoolTest()
        {
            ThreadPool.QueueUserWorkItem((o) => { });
            ThreadPool.UnsafeQueueUserWorkItem((o) => { }, null);
        }

        /// <summary>
        /// Used to sync threads across AppDomains.
        /// 
        /// You should pay attention to:
        /// - How to use it, this is most probably going to be on the test
        /// </summary>
        [TestMethod]
        public void MutexTest()
        {
            /// 33 times slower
            /// Used to syncronize applications
            // private static Mutex mut = new Mutex();

            int value = 0;

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; ++i)
            {
                Thread myThread = new Thread(new ThreadStart(() =>
                {
                    for (int f = 0; f < 10000; ++f)
                    {
                        UseResource(ref value);
                    }
                }));
                myThread.Name = String.Format("Thread #{0}", i + 1);
                threads[i] = myThread;
            }

            for (int j = 0; j < 10; j++)
            {
                threads[j].Start();
            }

            for (int j = 0; j < 10; j++)
            {
                threads[j].Join();
            }

            Assert.AreEqual(100000, value);
        }

        /// <summary>
        /// This method represents a resource that must be synchronized
        /// so that only one thread at a time can enter.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.threading.mutex.aspx"/>
        internal static void UseResource(ref int value)
        {
            // Wait until it is safe to enter.
            mut.WaitOne();

            Console.WriteLine("{0} has entered the protected area",
                Thread.CurrentThread.Name);

            // Place code to access non-reentrant resources here.
            ++value;

            Console.WriteLine("{0} is leaving the protected area\r\n",
                Thread.CurrentThread.Name);

            // Release the Mutex.
            mut.ReleaseMutex();
        }
    }
}
