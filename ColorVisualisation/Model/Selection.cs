using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model
{
    class Selection
    {
        private PixelContainer _pixelContainer;
        private SelectionScoringTable _scoringTable;      

        private int _pixelsToSelect;

        private double _pixelsToSelectRatio = 
            double.Parse(Resources.PixelsToSelectRatio);
        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);


        public Selection(PixelContainer pixelContainer)
        {
            _pixelContainer = pixelContainer;
            _pixelsToSelect = new NumberConversion().ToEvenNumber
                ((int)(_pixelContainer.Width * _pixelContainer.Height
                    / (_valuesInPixel * _pixelsToSelectRatio)));
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
            _pixelContainer.RemoveWeakPixels(_pixelsToSelect);
            return _pixelContainer;
        }
    }
}
