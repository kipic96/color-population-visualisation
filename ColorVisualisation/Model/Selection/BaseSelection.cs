using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Scoring;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model.Selection
{
    class BaseSelection
    {
        private PixelCollection _pixelContainer;
        private IScoringTable _scoringTable;

        private int _pixelsToSelect;

        private double _pixelsToSelectRatio = double.Parse(Resources.PixelsToSelectRatio);
        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);


        public BaseSelection(PixelCollection pixelContainer, int pixelsToSelect)
        {
            _pixelContainer = pixelContainer;
            _pixelsToSelect = pixelsToSelect;
            _scoringTable = new LinearScoringTable();     
        }

        public PixelCollection Execute()
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
