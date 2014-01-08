using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Compression;
using System.IO;

namespace Exam70_536
{
    /// <summary>
    /// 100% chance that the test will ask: How to zip a file with GZipStream or DeflateStream
    /// 
    /// You should pay attention to:
    /// - SYNTAX (you should know exactly the order of the parameters)
    /// - You should know how to read/write to streams
    /// </summary>
    [TestClass]
    public class CompressionTest
    {
        /// <summary>
        /// This class represents the gzip data format, which uses an industry standard algorithm for lossless
        /// file compression and decompression. The format includes a cyclic redundancy check value for detecting
        /// data corruption. The gzip data format uses the same algorithm as the DeflateStream class,
        /// but can be extended to use other compression formats. The format can be readily implemented
        /// in a manner not covered by patents.
        /// 
        /// Compressed GZipStream objects written to a file with an extension of .gz can be decompressed
        /// using many common compression tools; however, this class does not inherently provide functionality
        /// for adding files to or extracting files from .zip archives.
        /// 
        /// The GZipStream class cannot decompress data that results in over 8 GB of uncompressed data.
        /// 
        /// The compression functionality in DeflateStream and GZipStream is exposed as a stream. Data is read in
        /// on a byte-by-byte basis, so it is not possible to perform multiple passes to determine the best method
        /// for compressing entire files or large blocks of data. The DeflateStream and GZipStream classes are best
        /// used on uncompressed sources of data. If the source data is already compressed, using these classes may
        /// actually increase the size of the stream.
        /// 
        /// Notes to Inheritors
        /// When you inherit from GZipStream, you must override the following members: CanSeek, CanWrite, and CanRead.
        /// 
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.io.compression.gzipstream.aspx"/>
        [TestMethod]
        public void ZipTest()
        {
            string filename = "ftz.txt";
            string zipname = "ftz.gzip";

            // leaveOpen keeps the underlying stream opened
            // new GZipStream(Stream, CompressionMode.Compress  , leaveOpen)
            //                        CompressionMode.Decompress

            using (GZipStream zip = new GZipStream(File.Open(zipname, FileMode.Create), CompressionMode.Compress))
            {
                Compress(zip);
            }

            Assert.IsTrue(new FileInfo(zipname).Length < new FileInfo(filename).Length);
        }

        /// <summary>
        /// This class represents the Deflate algorithm, an industry standard algorithm for lossless
        /// file compression and decompression. It uses a combination of the LZ77 algorithm and Huffman coding.
        /// Data can be produced or consumed, even for an arbitrarily long, sequentially presented input data stream,
        /// using only previously bound amount of intermediate storage. The format can be implemented readily in a manner
        /// not covered by patents. For more information, see RFC 1951. "DEFLATE Compressed Data Format Specification version 1.3.
        /// 
        /// This class does not inherently provide functionality for adding files to or extracting files from .zip archives.
        /// 
        /// The GZipStream class uses the gzip data format, which includes a cyclic redundancy check value
        /// for detecting data corruption. The gzip data format uses the same compression algorithm as the DeflateStream class.
        /// 
        /// The compression functionality in DeflateStream and GZipStream is exposed as a stream.
        /// Data is read in on a byte-by-byte basis, so it is not possible to perform multiple passes to determine
        /// the best method for compressing entire files or large blocks of data. The DeflateStream and GZipStream
        /// classes are best used on uncompressed sources of data. If the source data is already compressed,
        /// using these classes may actually increase the size of the stream.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.io.compression.deflatestream.aspx"/>
        [TestMethod]
        public void DeflateTest()
        {
            string filename = "ftz.txt";
            string zipname = "ftz.deflate";

            using (DeflateStream zip = new DeflateStream(File.Open(zipname, FileMode.Create), CompressionMode.Compress))
            {
                Compress(zip);
            }

            Assert.IsTrue(new FileInfo(zipname).Length < new FileInfo(filename).Length);
        }

        /// <summary>
        /// Writes on the zip/deflate stream.
        /// </summary>
        /// <typeparam name="T">Type of the compression stream</typeparam>
        /// <param name="stream">Zip/Deflate Stream</param>
        internal void Compress<T>(T stream) where T : Stream
        {
            string filename = "ftz.txt";

            if (!File.Exists(filename))
            {
                // creates a big file
                using (var fs = new FileStream(filename, FileMode.Create))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(Enumerable.Range(0, 10000).Select(i => (char)(i%255)).ToArray());
                    
                    fs.Position = 5000000;
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                bytesRead = fs.Read(buffer, 0, 1024);
                stream.Write(buffer, 0, bytesRead);
            }
        }
    }
}
