using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Exam70_536
{
    /// <summary>
    /// Two different Timer classes
    /// </summary>
    [TestClass]
    public class TimersTest
    {
        [TestMethod]
        public void TimersTimerTest()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            bool result = false;

            timer.Elapsed += (s, e) =>
            {
                result = !result;

                timer.Stop();
                //timer.Dispose();
            };

            timer.Interval = 100;
            timer.Start();

            Thread.Sleep(200);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ThreadTimerTest()
        {
            bool result = false;

            System.Threading.Timer threadTimer =
                new System.Threading.Timer(
                    (obj) =>
                    {
                        result = true;
                    }
                , null, 300, 100);

            Assert.IsFalse(result);
            Thread.Sleep(350);
            Assert.IsTrue(result);

            threadTimer.Dispose();
        }
    }
}
