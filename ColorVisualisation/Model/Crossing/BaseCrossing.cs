using ColorVisualisation.Model.Entity;
using ColorVisualisation.Properties;
using System;
using System.Collections.Generic;

namespace ColorVisualisation.Model.Crossing
{
    abstract class BaseCrossing
    {
        protected IList<PixelPair> _pixelPairs;

        protected void RandomlyBindPixelToPairs(PixelCollection pixelContainer, int pixelsToSelect)
        {
            pixelContainer.OrderByPoints();
            _pixelPairs = new List<PixelPair>();
            var alreadySelectedPixelsIds = new List<int>();
            var randomGenerator = new Random();
            for (int index = 0; index <= pixelsToSelect - 1; index++)
            {
                if (!alreadySelectedPixelsIds.Contains(index))
                {
                    alreadySelectedPixelsIds.Add(index);
                    int newIndex = randomGenerator.Next(index + 1, pixelsToSelect);
                    while (alreadySelectedPixelsIds.Contains(newIndex))
                    {
                        newIndex = randomGenerator.Next(index + 1, pixelsToSelect);
                    }
                    alreadySelectedPixelsIds.Add(newIndex);
                    _pixelPairs.Add(new PixelPair()
                    {
                        First = pixelContainer.Pixels[index],
                        Second = pixelContainer.Pixels[newIndex]
                    });
                }
            }
            return;
        }

        public abstract PixelCollection Execute(PixelCollection pixelContainer, int pixelsToSelect, int howManyChildren);
    }
}
