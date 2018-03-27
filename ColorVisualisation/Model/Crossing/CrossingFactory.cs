using ColorVisualisation.Model.Entity;
using ColorVisualisation.Properties;
using System;

namespace ColorVisualisation.Model.Crossing
{
    class CrossingFactory
    {
        public static BaseCrossing Create(string crossingType)
        {
            if (crossingType == Resources.AverageCrossing)
                return new CrossingByAverage();
            if (crossingType == Resources.BitCrossing)
                return new CrossingByBit();
            throw new ArgumentException(Resources.ErrorCrossingType);
        }
    }
}
