using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Model.ScoringTable;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model
{
    class Selection
    {
        private PixelContainer _pixelContainer;
        private IScoringTable _scoringTable;

        private int _pixelsToSelect;

        private double _pixelsToSelectRatio = double.Parse(Resources.PixelsToSelectRatio);
        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);


        public Selection(PixelContainer pixelContainer, int pixelsToSelect)
        {
            _pixelContainer = pixelContainer;
            _pixelsToSelect = pixelsToSelect;
            _scoringTable = new LinearScoringTable();     
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
            _pixelContainer.OrderAscending();
            return _pixelContainer;
        }
    }
}
