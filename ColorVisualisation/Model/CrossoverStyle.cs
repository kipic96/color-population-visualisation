using ColorVisualisation.Properties;
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
            int endingPixel = _pixelsToSelect;
            for (int index = 0; index < endingPixel; index++, endingPixel--)
            {
                _pixelPairs.Add(new PixelPair()
                {
                    First = _pixelContainer.Pixels[index],
                    Second = _pixelContainer.Pixels[endingPixel - 1]
                });                
            }
            return;
        }
    }
}
