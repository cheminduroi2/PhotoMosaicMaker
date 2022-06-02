using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMosaicMaker;

namespace PhotoMosaicMakerTests
{
    [TestClass]
    public class ImageManagerTests
    {
        [TestMethod]
        public void Gets_Source_Images()
        {
            Dictionary<string, Bitmap> sourceImages = ImageManager.GetSourceImages(Constants.SourceImagesPath, Constants.SquareSideLength, Constants.SquareSideLength);
            //there should be 32 source images
            Assert.AreEqual(32, sourceImages.Count);
            Assert.IsTrue(sourceImages.ContainsKey("10443973_aeb97513fc_m.jpg"));
        }

        [TestMethod]
        public void Gets_Avg_RGB_Value()
        {
            Color testRGBValue = Color.FromArgb(1, 78, 72, 70);
            Bitmap img = new Bitmap(Constants.InputImagePath);
            Bitmap smallerSquareImg = ImageManager.GetSmallSquare(img, 0, 0, Constants.SquareSideLength, Constants.SquareSideLength);
            Color derivedRGBValue = ImageManager.GetAvgRGBValue(smallerSquareImg);
            Assert.AreEqual(testRGBValue, derivedRGBValue);
        }

        [TestMethod]
        public void Finds_RGB_Distance()
        {
            double testDistance = 255.0;
            double derivedDistance = ImageManager.FindRGBDistance(Color.Red, Color.Black);
            Assert.AreEqual(testDistance, derivedDistance);
        }
    }
}
