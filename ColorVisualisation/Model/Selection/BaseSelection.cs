using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Scoring;

namespace ColorVisualisation.Model.Selection
{
    class BaseSelection
    {
        public PixelCollection Execute(PixelCollection pixelCollection, IScoringTable scoringTable, int pixelsToSelect)
        {
            lock (pixelCollection)
            {
                pixelCollection.OrderByBlue();
                pixelCollection.AddPointsToValues(scoringTable);
                pixelCollection.OrderByGreen();
                pixelCollection.AddPointsToValues(scoringTable);
                pixelCollection.OrderByRed();
                pixelCollection.AddPointsToValues(scoringTable);
                pixelCollection.RemoveWeakPixels(pixelsToSelect);
                pixelCollection.OrderAscending();
            }            
            return pixelCollection;
        }
    }
}
