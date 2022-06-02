using System;
using System.Drawing;
using System.Collections.Generic;

namespace PhotoMosaicMaker
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            CreatePhotoMosaic();
        }

        public static void CreatePhotoMosaic()
        {
            Dictionary<string, Bitmap> sourceImages = ImageManager.GetSourceImages(Constants.SourceImagesPath, Constants.SquareSideLength, Constants.SquareSideLength);
            Dictionary<string, Color> srcImgsAvgRGBValues = ImageManager.AllAvgRGBValues(sourceImages);
            Bitmap img = new Bitmap(Constants.InputImagePath);
            using (Bitmap b = new Bitmap(img.Width, img.Height))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    for (int i = 0; i <= img.Width - Constants.SquareSideLength; i += Constants.SquareSideLength)
                    {
                        for (int j = 0; j <= img.Height - Constants.SquareSideLength; j += Constants.SquareSideLength)
                        {
                            Bitmap smallerSquareImg = ImageManager.GetSmallSquare(img, i == 0 ? i : i - 1, j == 0 ? j : j - 1, Constants.SquareSideLength, Constants.SquareSideLength);
                            Color smallSquareAvgRGB = ImageManager.GetAvgRGBValue(smallerSquareImg);
                            string respectiveSrcImg = ImageManager.MapToSourceImg(smallSquareAvgRGB, srcImgsAvgRGBValues);
                            g.DrawImage(sourceImages[respectiveSrcImg], new Point(i, j));
                        }
                    }

                }
                // download photomosaic to current directory mosaic-[UNIX_TIMESTAMP].jpg
                b.Save($"{Constants.SolutionPath}/mosaic-{(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds}.jpg");
            }
        }




    }
}