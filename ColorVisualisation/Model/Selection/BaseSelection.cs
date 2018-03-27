using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Scoring;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model.Selection
{
    class BaseSelection
    {
        public PixelCollection Execute(PixelCollection pixelContainer, IScoringTable scoringTable, int pixelsToSelect)
        {
            pixelContainer.OrderByBlue();
            pixelContainer.AddPointsToValues(scoringTable);
            pixelContainer.OrderByGreen();
            pixelContainer.AddPointsToValues(scoringTable);
            pixelContainer.OrderByRed();
            pixelContainer.AddPointsToValues(scoringTable);
            pixelContainer.RemoveWeakPixels(pixelsToSelect);
            pixelContainer.OrderAscending();
            return pixelContainer;
        }
    }
}
