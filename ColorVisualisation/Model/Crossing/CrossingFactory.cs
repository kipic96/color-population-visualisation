using ColorVisualisation.Model.Entity;
using ColorVisualisation.Properties;
using System;

namespace ColorVisualisation.Model.Crossing
{
    class CrossingFactory
    {
        public static BaseCrossing Create(string crossingType, PixelCollection pixelCollection,
            int pixelsToSelect, int howManyChildren)
        {
            if (crossingType == Resources.AverageCrossing)
                return new CrossingByAverage(pixelCollection, pixelsToSelect, howManyChildren);
            if (crossingType == Resources.BitCrossing)
                return new CrossingByBit(pixelCollection, pixelsToSelect, howManyChildren);
            throw new ArgumentException(Resources.ErrorCrossingType);
        }
    }
}
