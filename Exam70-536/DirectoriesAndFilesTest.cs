using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Security.AccessControl;

namespace Exam70_536
{
    [TestClass]
    public class DirectoriesAndFilesTest
    {
        private string testFileName = "file.txt";

        /// <summary>
        /// The static class File should be used only for "Single operations", to create a file and write to it
        /// create an instance of FileInfo instead.
        /// </summary>
        [TestMethod]
        public void StaticFileTest()
        {
            // File.Create(path: "test.txt", bufferSize: 100, options: FileOptions.None, fileSecurity: new FileSecurity());
            using(File.Create("TestFile"));

            Assert.IsTrue(File.Exists("TestFile"));

            // Rename a file
            File.Move("TestFile", "TestFileRenamed");

            Assert.IsFalse(File.Exists("TestFile"));

            File.Delete("TestFileRenamed");
            Assert.IsFalse(File.Exists("TestFileRenamed"));

            using (var streamWriter = File.CreateText("TestFile.txt"))
            {
                streamWriter.WriteLine("This is a text file");
            }

            Assert.IsTrue(File.Exists("TestFile.txt"));
            Assert.AreEqual(String.Format("This is a text file{0}", Environment.NewLine), File.ReadAllText("TestFile.txt"));

            File.Delete("TestFile.txt");

            Assert.IsFalse(File.Exists("TestFile.txt"));

            // Write contents with the right encoding
            File.WriteAllText("TestFile.txt", "This is a string", Encoding.UTF8);
            File.Delete("TestFile.txt");

            Assert.IsFalse(File.Exists("TestFile.txt"));
        }

        [TestMethod]
        public void FileStreamTest()
        {
            FileStream fs = new FileStream(testFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.DeleteOnClose);

            try
            {
                fs.WriteByte(65);
                Assert.AreEqual(1, fs.Position);

                fs.Seek(0, SeekOrigin.Begin);
                Assert.AreEqual(0, fs.Position);

                fs.ReadByte();
                Assert.AreEqual(1, fs.Position);
            }
            catch
            {
                Assert.Fail();
            }
            finally
            {
                fs.Dispose();
            }
        }

        /// <summary>
        /// You should pay attention to:
        /// - How to write
        /// - AutoFlush, Flush()
        /// </summary>
        [TestMethod]
        public void StreamWriterTest()
        {
            FileStream fs = new FileStream(testFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.DeleteOnClose);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.AutoFlush = true;
                sw.Write("A");
                sw.AutoFlush = false;

                sw.Write("B");
                sw.Flush();

                sw.Write("C");
            }
            catch
            {
                Assert.Fail();
            }
            finally
            {
                sw.Dispose();
                fs.Dispose();
            }
        }

        [TestMethod]
        public void FileCreateTest()
        {
            FileStream fs = new FileStream(testFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.DeleteOnClose);
            Assert.IsTrue(File.Exists(testFileName));
            fs.Dispose();

            Assert.IsFalse(File.Exists(testFileName));

            FileStream fs2 = File.Open(testFileName, FileMode.OpenOrCreate);
            Assert.IsTrue(File.Exists(testFileName));
            fs2.Dispose();

            File.Delete(testFileName);
            Assert.IsFalse(File.Exists(testFileName));
        }

        [TestMethod]
        public void FileInfoTest()
        {
            FileInfo fi = new FileInfo("TestFile");

            Assert.IsFalse(fi.Exists);

            using(fi.Create());

            // Rename a file
            fi.MoveTo("TestFile.txt");

            Assert.IsFalse(File.Exists("TestFile")); // renamed

            fi.Attributes |= FileAttributes.ReadOnly;

            Assert.IsTrue((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
            // fi.Attributes.HasFlag(FileAttributes.ReadOnly); // test probably won't ask this method, but it exists

            fi.Attributes &= ~FileAttributes.ReadOnly;

            Assert.IsFalse((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);

            // Write
            using (FileStream fs = fi.Open(FileMode.Create))
            {
                if (fs.CanWrite)
                {
                    byte[] bytesToWrite = Encoding.UTF8.GetBytes("Yorae Dragon");
                    fs.Write(bytesToWrite, 0, bytesToWrite.Length);
                }
            }

            // Read
            using (FileStream fs = fi.Open(FileMode.Open))
            {
                int bytesRead = 0, bytesToRead = 1000;
                byte[] buffer = new byte[bytesToRead];

                using (MemoryStream ms = new MemoryStream())
                {
                    do
                    {
                        bytesRead = fs.Read(buffer, 0, bytesToRead);
                        ms.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);

                    Assert.AreEqual(Encoding.UTF8.GetString(ms.ToArray()), "Yorae Dragon");
                }
            }

            fi.LastAccessTime = DateTime.Now.AddYears(-100);
            fi.LastWriteTime = DateTime.Now.AddYears(-100);
            fi.CreationTime = DateTime.Now.AddYears(100);

            Assert.IsTrue(fi.LastAccessTime < fi.CreationTime);

            fi.Delete();

            Assert.IsFalse(File.Exists("TestFile.txt"));
        }

        [TestMethod]
        public void FileTestList()
        {
            // will throw exception on folders without permission
            try
            {
                Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "*.*", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(f =>
                    {
                        var filename = Path.GetFileName(f);
                        var filenamW = Path.GetFileNameWithoutExtension(f);
                        var ext = Path.GetExtension(f);
                        var dirname = Path.GetDirectoryName(f);

                        bool debugger;
                    });
            }
            catch
            {
                Assert.IsTrue(true, "Not enough permission");
            }
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Gets files recursively ignoring errors.
        /// </summary>
        /// <remarks>Will fail if you don't have files on the desktop.</remarks>
        [TestMethod]
        public void FileTestListAll()
        {
            Dictionary<string, IEnumerable<string>> tree = new Dictionary<string, IEnumerable<string>>();

            DirectoryInfo root = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            FileTestListRecursive(root, tree);

            Assert.IsTrue(tree.Count > 0);
        }

        internal void FileTestListRecursive(DirectoryInfo root, Dictionary<string, IEnumerable<string>> tree)
        {
            DirectoryInfo[] directories = null;
            try
            {
                directories = root.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (var dir in directories)
            {
                try
                {
                    tree.Add(dir.FullName, Directory.GetFiles(root.FullName));
                }
                catch { }

                FileTestListRecursive(dir, tree);
            }
        }

    }
}
