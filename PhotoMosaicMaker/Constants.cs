using System;
using System.IO;

namespace PhotoMosaicMaker
{
    public class Constants
    {
        public static readonly string
            SolutionPath = Directory.GetParent(
                Environment.CurrentDirectory
            ).Parent.FullName.Split(
                "/PhotoMosaicMaker"
            )[0] + "/PhotoMosaicMaker",
            InputImagePath = SolutionPath + "/test-image.jpg",
            SourceImagesPath = SolutionPath + "/Sourceimages";
        public const int SquareSideLength = 20;
    }
}
