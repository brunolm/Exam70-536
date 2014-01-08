using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Exam70_536
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        public void ProcessInfoTest()
        {
            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                Assert.IsTrue(process.WorkingSet64 > 0);
            }
        }

        [TestMethod]
        public void StartProcessTest()
        {
            var process = new Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "ipconfig";

            bool started = process.Start();
            string ip = "0.0.0.0";

            using (var sr = process.StandardOutput)
            {
                ip = Regex.Match(sr.ReadToEnd(), @"(?<ip>\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3})").Groups["ip"].Value;
            }
            process.WaitForExit();

            Assert.AreNotEqual<string>("0.0.0.0", ip);
        }
    }
}
