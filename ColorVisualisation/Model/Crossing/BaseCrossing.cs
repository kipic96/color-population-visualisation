using ColorVisualisation.Model.Entity;
using ColorVisualisation.Properties;
using System;
using System.Collections.Generic;

namespace ColorVisualisation.Model.Crossing
{
    abstract class BaseCrossing
    {
        protected PixelCollection _pixelContainer;
        protected IList<PixelPair> _pixelPairs;
        protected int _pixelsToSelect;
        protected int _howManyChildren;
        protected int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);

        public BaseCrossing(PixelCollection pixelContainer, int pixelsToSelect, int howManyChildren)
        {
            _pixelContainer = pixelContainer;
            _pixelsToSelect = pixelsToSelect;
            _howManyChildren = howManyChildren;
        }

        protected void BindPixelToPairs()
        {
            _pixelContainer.OrderByPoints();
            _pixelPairs = new List<PixelPair>();
            var alreadySelectedPixelsIds = new List<int>();
            var randomGenerator = new Random();
            for (int index = 0; index <= _pixelsToSelect - 1; index++)
            {
                if (!alreadySelectedPixelsIds.Contains(index))
                {
                    alreadySelectedPixelsIds.Add(index);
                    int newIndex = randomGenerator.Next(index + 1, _pixelsToSelect);
                    while (alreadySelectedPixelsIds.Contains(newIndex))
                    {
                        newIndex = randomGenerator.Next(index + 1, _pixelsToSelect);
                    }
                    alreadySelectedPixelsIds.Add(newIndex);
                    _pixelPairs.Add(new PixelPair()
                    {
                        First = _pixelContainer.Pixels[index],
                        Second = _pixelContainer.Pixels[newIndex]
                    });
                }
            }
            return;
        }

        public abstract PixelCollection Execute();
    }
}
