using ColorVisualisation.Properties;
using System;
using System.Collections.Generic;

namespace ColorVisualisation.Model
{
    abstract class CrossoverStyle
    {
        protected PixelContainer _pixelContainer;
        protected IList<PixelPair> _pixelPairs;
        protected int _pixelsToSelect;
        protected int _howManyChildren;
        protected int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);

        public CrossoverStyle(PixelContainer pixelContainer, int pixelsToSelect, int howManyChildren)
        {
            _pixelContainer = pixelContainer;
            _pixelsToSelect = pixelsToSelect;
            _howManyChildren = howManyChildren;
        }

        public abstract PixelContainer Execute();

        protected void BondPixelToPairs()
        {
            _pixelContainer.OrderByPoints();
            _pixelPairs = new List<PixelPair>();
            var alreadySelectedPixelsIds = new List<int>();
            var randomGenerator = new Random();
            int endingPixel = _pixelsToSelect;
            /*for (int index = 0; index < endingPixel; index++, endingPixel--)
            {
                _pixelPairs.Add(new PixelPair()
                {
                    First = _pixelContainer.Pixels[index],
                    Second = _pixelContainer.Pixels[endingPixel - 1]
                });                
            }*/            
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
    }
}
