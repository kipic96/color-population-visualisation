using System;
using ColorVisualisation.Model.Entity;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model.Crossing
{
    class CrossingByAverage : BaseCrossing
    {
        public CrossingByAverage(PixelCollection pixelContainer, int pixelsToSelect, int howManyChildren) 
            : base(pixelContainer, pixelsToSelect, howManyChildren) { }

        public override PixelCollection Execute()
        {
            BindPixelToPairs();
            PixelCrossing();
            return _pixelContainer;
        }

        private void PixelCrossing()
        {
            int deadPixelIndex = _pixelsToSelect;
            foreach (var pixelPair in _pixelPairs)
            {
                for (int i = 0; i < _howManyChildren; i++)
                {
                    var newPixel = new Pixel()
                    {
                        Blue = Average(pixelPair.First.Blue, pixelPair.Second.Blue),
                        Green = Average(pixelPair.First.Green, pixelPair.Second.Green),
                        Red = Average(pixelPair.First.Red, pixelPair.Second.Red),
                        Alpha = byte.MaxValue,
                        IndexColumn = _pixelContainer.Pixels[deadPixelIndex].IndexColumn,
                        IndexRow = _pixelContainer.Pixels[deadPixelIndex].IndexRow,
                        IndexGlobal = _pixelContainer.Pixels[deadPixelIndex].IndexGlobal,
                        RankingPoints = 0,
                    };
                    _pixelContainer.Pixels[deadPixelIndex] = newPixel;
                    deadPixelIndex++;
                }
            }
            _pixelContainer.OrderAscending();
            return;
        }

        private int Average(int first, int second)
        {
            return (int)((first + second) / 2);
        }
    }
}
