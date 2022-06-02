using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace PhotoMosaicMaker
{
    public static class ImageManager
    {
        public static Dictionary<string, Bitmap> GetSourceImages(string sourceImagesPath, int width, int height)
        {
            string[] sourceImageNames = Directory.GetFiles(sourceImagesPath, "*.jpg");
            Dictionary<string, Bitmap> sourceImages = new Dictionary<string, Bitmap>();
            
            for (int i = 0; i < sourceImageNames.Length; i++)
            {
                Bitmap srcImg = new Bitmap(sourceImageNames[i]);
                sourceImages.Add(
                    Path.GetFileName(
                        sourceImageNames[i]),
                        srcImg.GetThumbnailImage(
                            width,
                            height,
                            null,
                            IntPtr.Zero
                        ) as Bitmap
                    );
            }
            return sourceImages;
        }

        public static Color GetAvgRGBValue(Bitmap b)
        {
            int rTotal = 0, gTotal = 0, bTotal = 0;
            int totalPixels = b.Width * b.Height;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color pixel = b.GetPixel(i, j);
                    rTotal += pixel.R;
                    gTotal += pixel.G;
                    bTotal += pixel.B;
                }
            }
            return Color.FromArgb(1, rTotal / totalPixels, gTotal / totalPixels, bTotal / totalPixels);
        }

        public static Dictionary<string, Color> AllAvgRGBValues(Dictionary<string, Bitmap> images)
        {
            Dictionary<string, Color> rgbValues = new Dictionary<string, Color>();
            foreach (KeyValuePair<string, Bitmap> entry in images)
            {
                rgbValues.Add(entry.Key, GetAvgRGBValue(entry.Value));
            }
            return rgbValues;
        }

        public static Bitmap GetSmallSquare(Bitmap img, int x, int y, int width, int height)
        {

            Bitmap imgTemp = new Bitmap(img);
            Rectangle cloneRect = new Rectangle(x, y, width, height);
            PixelFormat format = imgTemp.PixelFormat;
            Bitmap smallSquareImage = imgTemp.Clone(cloneRect, format);
            return smallSquareImage;
        }

        public static double FindRGBDistance(Color pixelA, Color pixelB)
        {
            return Math.Pow(
                Math.Pow(pixelA.R - pixelB.R, 2) +
                Math.Pow(pixelA.G - pixelB.G, 2) +
                Math.Pow(pixelA.B - pixelB.B, 2),
                0.5
            );
        }

        // finds the appropriate (smallest distance in average RGB) source image for this square
        // sub-section of the original image
        public static string MapToSourceImg(Color ogImageSquare, Dictionary<string, Color> srcImages)
        {
            string srcImgName = "";
            double lowestRGBDistance = -1;

            foreach (KeyValuePair<string, Color> entry in srcImages)
            {
                double rgbDistance = FindRGBDistance(ogImageSquare, entry.Value);
                if (lowestRGBDistance < 0 || rgbDistance < lowestRGBDistance)
                {
                    srcImgName = entry.Key;
                    lowestRGBDistance = rgbDistance;
                }
            }
            return srcImgName;
        }
    }
}