using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model
{
    class Selection
    {
        private PixelContainer _pixelContainer;
        private SelectionScoringTable _scoringTable;      

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
            _scoringTable = new SelectionScoringTable();     
        }

        public PixelContainer Execute()
        {
            _pixelContainer.OrderByBlue();
            _pixelContainer.AddPointsToValues(_scoringTable);
            _pixelContainer.OrderByGreen();
            _pixelContainer.AddPointsToValues(_scoringTable);
            _pixelContainer.OrderByRed();
            _pixelContainer.AddPointsToValues(_scoringTable);
            return _pixelContainer.GetTopPixels(_pixelsToSelect);
        }
    }
}
