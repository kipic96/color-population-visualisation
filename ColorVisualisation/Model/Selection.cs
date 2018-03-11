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
        private PixelContainer _pixelContainer;        

        private int _pixelsToSelect;
        private int[,] _selectedPixelsIndexes;

        int _allPixelsNumber;
        private double _pixelsToSelectRatio = 
            double.Parse(Resources.PixelsToSelectRatio);
        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);
        private int _height = int.Parse(Resources.BitmapHeight);
        private int _width = int.Parse(Resources.BitmapWidth);


        public Selection(PixelContainer pixelContainer)
        {
            _pixelContainer = pixelContainer;
            _allPixelsNumber = _width * _height;
            _pixelsToSelect = new NumberConversion().ToEvenNumber
                ((int)(_allPixelsNumber / (_valuesInPixel * _pixelsToSelectRatio)));
            _selectedPixelsIndexes = new int[_pixelsToSelect, 2];           
            
        }

        public int[,] Execute()
        {
            SortPixels();

            // TODO Selection algorithm
            return _selectedPixelsIndexes;
        }

        private void SortPixels()
        {
            _pixelContainer.CreateRanking();
            /*_bluePixels = _bluePixels.OrderBy(x => Math.Abs(x - _averageBlue)).ToArray();
            _greenPixels.OrderBy(x => Math.Abs(x - _averageGreen)).ToArray();
            _redPixels.OrderBy(x => Math.Abs(x - _averageRed)).ToArray();*/

        }
    }
}
