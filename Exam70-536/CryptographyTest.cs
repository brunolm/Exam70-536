using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.IO;

namespace Exam70_536
{
    /// <summary>
    /// The System.Security.Cryptography namespace provides cryptographic services, including secure encoding
    /// and decoding of data, as well as many other operations, such as hashing, random number generation,
    /// and message authentication.
    /// 
    /// You should pay attention to:
    /// - Constructor
    /// - Usage
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/system.security.cryptography.aspx"/>
    [TestClass]
    public class CryptographyTest
    {
        /// <summary>
        /// DES       = 64
        /// </summary>
        [TestMethod]
        public void DESTest()
        {
            DES encrypter = DES.Create();

            string resultEnc = "This is my password";
            string resultDec = "";
            byte[] encryptedBytes = new byte[20];

            using (var ms = new MemoryStream())
            using (CryptoStream crypt = new CryptoStream(ms, encrypter.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(crypt))
                {
                    sw.Write(resultEnc);
                    sw.Flush();
                }

                encryptedBytes = ms.ToArray();
                resultEnc = Encoding.UTF8.GetString(encryptedBytes);
            }

            using (var ms = new MemoryStream(encryptedBytes))
            {
                using (var crypt = new CryptoStream(ms, encrypter.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(crypt))
                    {
                        resultDec = sr.ReadToEnd();
                    }
                }
            }

            Assert.AreEqual("This is my password", resultDec);
        }

        /// <summary>
        /// RC2       = 128
        /// </summary>
        [TestMethod]
        public void RC2Test()
        {
            RC2 encrypter = RC2.Create();

            string resultEnc = "This is my password";
            string resultDec = "";
            byte[] encryptedBytes = new byte[20];

            using (var ms = new MemoryStream())
            using (CryptoStream crypt = new CryptoStream(ms, encrypter.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(crypt))
                {
                    sw.Write(resultEnc);
                    sw.Flush();
                }

                encryptedBytes = ms.ToArray();
                resultEnc = Encoding.UTF8.GetString(encryptedBytes);
            }

            using (var ms = new MemoryStream(encryptedBytes))
            {
                using (var crypt = new CryptoStream(ms, encrypter.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(crypt))
                    {
                        resultDec = sr.ReadToEnd();
                    }
                }
            }

            Assert.AreEqual("This is my password", resultDec);
        }

        /// <summary>
        /// TRIPLEDES = 192
        /// </summary>
        [TestMethod]
        public void TripleDESTest()
        {
            TripleDES encrypter = TripleDES.Create();

            string resultEnc = "This is my password";
            string resultDec = "";
            byte[] encryptedBytes = new byte[20];

            using (var ms = new MemoryStream())
            using (CryptoStream crypt = new CryptoStream(ms, encrypter.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(crypt))
                {
                    sw.Write(resultEnc);
                    sw.Flush();
                }

                encryptedBytes = ms.ToArray();
                resultEnc = Encoding.UTF8.GetString(encryptedBytes);
            }

            using (var ms = new MemoryStream(encryptedBytes))
            {
                using (var crypt = new CryptoStream(ms, encrypter.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(crypt))
                    {
                        resultDec = sr.ReadToEnd();
                    }
                }
            }

            Assert.AreEqual("This is my password", resultDec);
        }

        /// <summary>
        /// RIJNDAEL  = 256
        /// </summary>
        [TestMethod]
        public void RIJNDAELTest()
        {
            Rijndael encrypter = Rijndael.Create();

            string resultEnc = "This is my password";
            string resultDec = "";
            byte[] encryptedBytes = new byte[20];

            using (var ms = new MemoryStream())
            using (CryptoStream crypt = new CryptoStream(ms, encrypter.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(crypt))
                {
                    sw.Write(resultEnc);
                    sw.Flush();
                }

                encryptedBytes = ms.ToArray();
                resultEnc = Encoding.UTF8.GetString(encryptedBytes);
            }

            using (var ms = new MemoryStream(encryptedBytes))
            {
                using (var crypt = new CryptoStream(ms, encrypter.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(crypt))
                    {
                        resultDec = sr.ReadToEnd();
                    }
                }
            }

            Assert.AreEqual("This is my password", resultDec);
        }

        [TestMethod]
        public void HashTest()
        {
            string password = "abcd1234";
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(passwordBytes);

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            sha1.ComputeHash(passwordBytes);

            SHA256 sha256 = new SHA256CryptoServiceProvider();
            sha256.ComputeHash(passwordBytes);

            SHA384 sha384 = new SHA384CryptoServiceProvider();
            sha384.ComputeHash(passwordBytes);

            SHA512 sha512 = new SHA512CryptoServiceProvider();
            sha512.ComputeHash(passwordBytes);


            Assert.AreNotEqual<byte[]>(md5.Hash, sha1.Hash);
            Assert.AreNotEqual<byte[]>(md5.Hash, sha256.Hash);
        }
    }
}
