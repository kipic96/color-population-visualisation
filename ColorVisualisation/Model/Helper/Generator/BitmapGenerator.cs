using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Helper.Extension;
using System;
using System.Collections.Generic;

namespace ColorVisualisation.Model.Helper.Generator
{
    class PixelsGenerator
    {
        private int _pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
        private int _dpi = int.Parse(Properties.Resources.DPI);

        public static PixelCollection Generate(int Width, int Height)
        {
            IList<Pixel> generatedPixels = new List<Pixel>();
            var numberGenerator = new Random();
            int pixelIndex = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    generatedPixels.Add(new Pixel()
                    {
                        Blue = numberGenerator.NextByte(),
                        Green = numberGenerator.NextByte(),
                        Red = numberGenerator.NextByte(),
                        Alpha = byte.MaxValue,
                        IndexColumn = col,
                        IndexRow = row,
                        IndexGlobal = pixelIndex,
                    });
                    pixelIndex++;
                }
            }
            var readyCollection = new PixelCollection(generatedPixels)
            {
                Width = Width,
                Height = Height
            };
            return readyCollection;
        }
    }
}
