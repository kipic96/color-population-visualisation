using ColorVisualisation.Model.Entity;
using System;
using System.Collections.Generic;

namespace ColorVisualisation.Model.Crossing
{
    abstract class BaseCrossing
    {
        protected IList<PixelPair> _pixelPairs;

        protected void RandomlyBindPixelsIntoPairs(PixelCollection pixelCollection, int pixelsToSelect)
        {
            pixelCollection.OrderByPoints();
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
                        First = pixelCollection[index],
                        Second = pixelCollection[newIndex],
                    });
                }
            }
            return;
        }

        public PixelCollection Execute(PixelCollection pixelCollection, int pixelsToSelect, int howManyChildren)
        {
            lock (pixelCollection)
            {
                RandomlyBindPixelsIntoPairs(pixelCollection, pixelsToSelect);
                PixelCrossing(pixelCollection, pixelsToSelect, howManyChildren);
                return pixelCollection;
            }            
        }

        protected abstract void PixelCrossing(PixelCollection pixelCollection, int pixelsToSelect, int howManyChildren);
    }
}
