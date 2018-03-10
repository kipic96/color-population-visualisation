using ColorVisualisation.Model.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVisualisation.Model
{
    class Selection
    {
        private byte[,,] _rawPixels;

        private byte averageBlue;
        private byte averageGreen;
        private byte averageRed;

        private double pixelsToSelectRatio = 
            double.Parse(Properties.Resources.PixelsToSelectRatio);
        private _height =
            (int)(Properties.Resources.Bitmap)

        public Selection(byte[,,] rawPixels)
        {
            _rawPixels = rawPixels;
        }

        public int[,] Execute()
        {
            int pixelsToSelect =  new NumberConversion().ToEvenNumber
                ((int)(_rawPixels.Length / 4 * pixelsToSelectRatio));
            var selectedPixelsIndexes = new int[pixelsToSelect, 2];

            return selectedPixelsIndexes;
        }

        private void CalculateAverageRGBValues()
        {
            _rawPixels.
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    for (int i = 0; i < _pixelValues; i++)
                        rawPixels[row, col, i] = byte.MaxValue;
                }
            }
        }
    }
}
