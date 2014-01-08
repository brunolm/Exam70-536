using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Exam70_536
{
    /// <summary>
    /// You should pay attention to:
    /// - Mainly the tricky questions, read the regex several times
    /// </summary>
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void ReplaceHttpTest()
        {
            string testStr = @"<a href='HTtP://www.microsoft.com'>Microsoft</a> htTP://www.bing.com/";

            testStr = Regex.Replace(testStr, @"http://", @"https://", RegexOptions.IgnoreCase);

            Assert.AreEqual<string>(@"<a href='https://www.microsoft.com'>Microsoft</a> https://www.bing.com/", testStr);
        }

        [TestMethod]
        public void GroupTest()
        {
            string s = @"First Name: Bruno";

            string p = @"First Name: (?<firstName>.+)";

            Match m = Regex.Match(s, p);

            Assert.AreEqual("Bruno", m.Groups["firstName"].Value);
            Assert.AreEqual("Bruno", m.Groups["firstName"].ToString());
        }

        [TestMethod]
        public void AmotzTest()
        {
            var pattern = @"zo*t$";

            Assert.IsTrue(Regex.IsMatch("zot", pattern));
            Assert.IsTrue(Regex.IsMatch("zoot", pattern));
        }

        [TestMethod]
        public void AmomomottothezTest()
        {
            var pattern = @"^a(mo)+t.*z$";

            Assert.IsTrue(Regex.IsMatch("amotz", pattern));
            Assert.IsFalse(Regex.IsMatch("amomtrewz", pattern));
            Assert.IsTrue(Regex.IsMatch("amotmoz", pattern));
            Assert.IsFalse(Regex.IsMatch("atrewz", pattern));
            Assert.IsTrue(Regex.IsMatch("amomomottothez", pattern));

        }
    }
}
