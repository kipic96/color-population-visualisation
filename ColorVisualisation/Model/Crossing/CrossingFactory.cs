using ColorVisualisation.Properties;
using System;

namespace ColorVisualisation.Model.Crossing
{
    class CrossingFactory
    {
        public static BaseCrossing Create(string crossingType)
        {
            if (crossingType == Resources.AverageCrossing)
                return new AverageCrossing();
            if (crossingType == Resources.BitCrossing)
                return new BitCrossing();
            throw new ArgumentException(Resources.ErrorCrossingType);
        }
    }
}
