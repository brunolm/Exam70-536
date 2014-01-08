using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam70_536
{
    [TestClass]
    public class EncodingTest
    {
        internal readonly string test = "It's laughing, a cheese covered scorpion! 神　風　火　水　狐　影　人　ブレスオブファイアＩＶ";
        internal readonly byte[] testASCIIEncoded;
        internal readonly byte[] testUTF7Encoded;
        internal readonly byte[] testUTF8Encoded;
        internal readonly byte[] testUnicodeEncoded;
        internal readonly byte[] testUTF32Encoded;

        public EncodingTest()
        {
            testASCIIEncoded = Encoding.ASCII.GetBytes(test);
            testUTF7Encoded = Encoding.UTF7.GetBytes(test);
            testUTF8Encoded = Encoding.UTF8.GetBytes(test);
            testUnicodeEncoded = Encoding.Unicode.GetBytes(test);
            testUTF32Encoded = Encoding.UTF32.GetBytes(test);
        }

        [TestMethod]
        public void ASCIITest()
        {
            // the test was encoded by default with UTF8, so it wouldn't work for this test
            string testASCII = Encoding.ASCII.GetString(testASCIIEncoded);

            Assert.AreEqual(testASCII, Encoding.ASCII.GetString(testASCIIEncoded));
            Assert.AreEqual(testASCII, Encoding.UTF7.GetString(testASCIIEncoded));
            Assert.AreEqual(testASCII, Encoding.UTF8.GetString(testASCIIEncoded));
            Assert.AreNotEqual(testASCII, Encoding.Unicode.GetString(testASCIIEncoded));
            Assert.AreNotEqual(testASCII, Encoding.UTF32.GetString(testASCIIEncoded));
        }

        [TestMethod]
        public void UTF7Test()
        {
            Assert.AreNotEqual(test, Encoding.ASCII.GetString(testUTF7Encoded));
            Assert.AreEqual(test, Encoding.UTF7.GetString(testUTF7Encoded));
            Assert.AreNotEqual(test, Encoding.UTF8.GetString(testUTF7Encoded));
            Assert.AreNotEqual(test, Encoding.Unicode.GetString(testUTF7Encoded));
            Assert.AreNotEqual(test, Encoding.UTF32.GetString(testUTF7Encoded));
        }

        [TestMethod]
        public void UTF8Test()
        {
            Assert.AreNotEqual(test, Encoding.ASCII.GetString(testUTF8Encoded));
            Assert.AreNotEqual(test, Encoding.UTF7.GetString(testUTF8Encoded));
            Assert.AreEqual(test, Encoding.UTF8.GetString(testUTF8Encoded));
            Assert.AreNotEqual(test, Encoding.Unicode.GetString(testUTF8Encoded));
            Assert.AreNotEqual(test, Encoding.UTF32.GetString(testUTF8Encoded));
        }

        [TestMethod]
        public void UnicodeTest()
        {
            Assert.AreNotEqual(test, Encoding.ASCII.GetString(testUnicodeEncoded));
            Assert.AreNotEqual(test, Encoding.UTF7.GetString(testUnicodeEncoded));
            Assert.AreNotEqual(test, Encoding.UTF8.GetString(testUnicodeEncoded));
            Assert.AreEqual(test, Encoding.Unicode.GetString(testUnicodeEncoded));
            Assert.AreNotEqual(test, Encoding.UTF32.GetString(testUnicodeEncoded));
        }

        [TestMethod]
        public void UTF32Test()
        {
            Assert.AreNotEqual(test, Encoding.ASCII.GetString(testUTF32Encoded));
            Assert.AreNotEqual(test, Encoding.UTF7.GetString(testUTF32Encoded));
            Assert.AreNotEqual(test, Encoding.UTF8.GetString(testUTF32Encoded));
            Assert.AreNotEqual(test, Encoding.Unicode.GetString(testUTF32Encoded));
            Assert.AreEqual(test, Encoding.UTF32.GetString(testUTF32Encoded));
        }

        [TestMethod]
        public void EncodingListTest()
        {
            string[] encodings = Encoding.GetEncodings().Select(o => String.Format("{0} - {1}", o.Name, o.DisplayName)).ToArray();

            foreach (var encodingName in encodings)
            {
                Assert.IsTrue(encodingName.Length > 0);
            }
        }
    }
}
