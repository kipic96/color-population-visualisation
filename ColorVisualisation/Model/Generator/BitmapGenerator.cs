
using ColorVisualisation.Model.Extension;
using System;

namespace ColorVisualisation.Model.Generator
{
    class BitmapGenerator
    {
        private int _width = int.Parse(Properties.Resources.BitmapWidth);
        private int _height = int.Parse(Properties.Resources.BitmapHeight);
        private int _pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
        private int _dpi = int.Parse(Properties.Resources.DPI);

        public byte[,,] Generate()
        {
            var rawPixels = new byte[_height, _width, _pixelValues];

            var numberGeneator = new Random();
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    for (int i = 0; i < _pixelValues - 1; i++)
                        rawPixels[row, col, i] = numberGeneator.NextByte();
                    rawPixels[row, col, _pixelValues - 1] = byte.MaxValue;
                }
            }        

            return rawPixels;
        }

        public byte[,,] Empty()
        {
            var rawPixels = new byte[_height, _width, _pixelValues];

            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    for (int i = 0; i < _pixelValues; i++)
                        rawPixels[row, col, i] = byte.MaxValue;
                }
            }

            return rawPixels;
        }
    }
}
