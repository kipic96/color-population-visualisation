
using ColorVisualisation.Model.Extension;
using System;
using System.Collections.Generic;

namespace ColorVisualisation.Model.Generator
{
    class PixelsGenerator
    {
        private int _width;
        private int _height;
        private int _pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
        private int _dpi = int.Parse(Properties.Resources.DPI);

        public PixelsGenerator(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public PixelContainer Generate()
        {
            IList<Pixel> generatedPixels = new List<Pixel>();
            var numberGenerator = new Random();
            int pixelIndex = 0;
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
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
            var readyCollection = new PixelContainer()
            {
                Pixels = generatedPixels,
                Width = _width,
                Height = _height
            };
            return readyCollection;
        }
    }
}
