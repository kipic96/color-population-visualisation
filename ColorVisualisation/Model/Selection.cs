using ColorVisualisation.Model.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using ColorVisualisation.Properties;
using System.Windows;

namespace ColorVisualisation.Model
{
    class Selection
    {
        private int[] _bluePixels;
        private int[] _greenPixels;
        private int[] _redPixels;

        private int _averageBlue;
        private int _averageGreen;
        private int _averageRed;

        private int _pixelsToSelect;
        private int[,] _selectedPixelsIndexes;

        int _allPixelsNumber;
        private double _pixelsToSelectRatio = 
            double.Parse(Resources.PixelsToSelectRatio);
        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);
        private int _height = int.Parse(Resources.BitmapHeight);
        private int _width = int.Parse(Resources.BitmapWidth);


        public Selection(byte[,,] rawPixels)
        {
            _allPixelsNumber = _width * _height;
            _pixelsToSelect = new NumberConversion().ToEvenNumber
                ((int)(_allPixelsNumber / _valuesInPixel * _pixelsToSelectRatio));
            _selectedPixelsIndexes = new int[_pixelsToSelect, 2];
            _bluePixels = new int[_allPixelsNumber];
            _greenPixels = new int[_allPixelsNumber];
            _redPixels = new int[_allPixelsNumber];
            // Splitting rawPixels into three arrays
            int pixelIndex = 0;
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    _bluePixels[pixelIndex] = rawPixels[row, col, 0];
                    _greenPixels[pixelIndex] = rawPixels[row, col, 1];
                    _redPixels[pixelIndex] = rawPixels[row, col, 2];
                    pixelIndex++;
                }
            }
        }

        public int[,] Execute()
        {
            

            CalculateAverageRGBValues();
            ChooseBestPixels();

            // TODO Selection algorithm
            return _selectedPixelsIndexes;
        }

        private void CalculateAverageRGBValues()
        {
            _averageBlue = (int)_bluePixels.Average();
            _averageGreen = (int)_greenPixels.Average();
            _averageRed = (int)_redPixels.Average();
        }

        private void ChooseBestPixels()
        {

        }
    }
}
