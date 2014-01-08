using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;

namespace Exam70_536
{
    [TestClass]
    public class DrawingTest
    {
        private string sampleImageFile = @"..\..\..\Exam70-536\Files\Sample.jpg";

        [TestMethod]
        public void ImageGrayscale()
        {
            TimeSpan unsafeTs;
            TimeSpan matrixTs;

            using (Image img = Bitmap.FromFile(sampleImageFile))
            {
                img.Save("Original.jpg");

                Image gray = null;
                unsafeTs = Performance.Test("Unsafe grayscale", 1, false,
                    () =>
                    {
                        gray = GrayscaleUnsafe(img);
                    });

                gray.Save("SampleUnsafe.jpg");
                gray.Dispose();

                matrixTs = Performance.Test("Matrix grayscale", 1, false,
                    () =>
                    {
                        gray = GrayscaleMatrix(img);
                    });

                gray.Save("SampleMatrix.jpg");
                gray.Dispose();
            }

            //Process.Start(".");
            Assert.IsTrue(unsafeTs < matrixTs);
        }

        [TestMethod]
        public void DrawObjectsTest()
        {
            Image sample = Image.FromFile(sampleImageFile);

            Graphics g = Graphics.FromImage(sample);

            g.DrawString("Fou-lu: Dragon God"
                , new Font(new FontFamily("Verdana"), 18)
                , new LinearGradientBrush(new Point(0, 0), new Point(100, 1), Color.Lime, Color.Blue)
                , new Point(10, 10));

            sample.Save("Modified.jpg");

            g.Dispose();
            sample.Dispose();

            // Process.Start(".");

            Assert.IsTrue(File.Exists("Modified.jpg"));
        }

        [TestMethod]
        public void GeneralTest()
        {
            Image sample = Image.FromFile(sampleImageFile);

            Graphics g = Graphics.FromImage(sample);

            g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 10, 50));
            g.DrawPolygon(new Pen(Color.Lime), new[] { new Point(20, 20), new Point(30, 40), new Point(10, 40), new Point(20, 20) });
            g.DrawPie(new Pen(Color.LightSteelBlue), new Rectangle(40, 40, 40, 40), 0, 10);

            g.Dispose();
            sample.Dispose();
        }

        /// <summary>
        /// Fastest method
        /// </summary>
        /// <param name="image">Image to set gray</param>
        /// <see cref="http://www.codeproject.com/Articles/13820/Painless-yet-unsafe-grayscale-conversion-in-C"/>
        /// <returns></returns>
        public Bitmap GrayscaleUnsafe(Bitmap image)
        {
            Bitmap returnMap = new Bitmap(image.Width, image.Height,
                                   PixelFormat.Format32bppArgb);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0,
                                     image.Width, image.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            int a = 0;
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        a = (imagePointer1[0] + imagePointer1[1] +
                             imagePointer1[2]) / 3;
                        imagePointer2[0] = (byte)a;
                        imagePointer2[1] = (byte)a;
                        imagePointer2[2] = (byte)a;
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride -
                                    (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride -
                                    (bitmapData1.Width * 4);
                }//end for i
            }//end unsafe
            returnMap.UnlockBits(bitmapData2);
            image.UnlockBits(bitmapData1);
            return returnMap;
        }//end processImage
        public Bitmap GrayscaleUnsafe(Image image)
        {
            return GrayscaleUnsafe(image as Bitmap);
        }

        /// <summary>
        /// Recommended Grayscale algoritm
        /// </summary>
        /// <param name="original"></param>
        /// <see cref="http://www.switchonthecode.com/tutorials/csharp-tutorial-convert-a-color-image-to-grayscale"/>
        /// <returns></returns>
        public static Bitmap GrayscaleMatrix(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][] 
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                }
            );

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        public static Bitmap GrayscaleMatrix(Image original)
        {
            return GrayscaleMatrix(original as Bitmap);
        }
    }
}
