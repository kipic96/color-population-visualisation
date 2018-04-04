using ColorVisualisation.Model.Entity;

namespace ColorVisualisation.Model.Crossing
{
    class CrossingByAverage : BaseCrossing
    {
        public override PixelCollection Execute(PixelCollection pixelContainer, int pixelsToSelect, int howManyChildren)
        {
            RandomlyBindPixelToPairs(pixelContainer, pixelsToSelect);
            PixelCrossing(pixelContainer, pixelsToSelect, howManyChildren);
            return pixelContainer;
        }

        private void PixelCrossing(PixelCollection pixelContainer, int pixelsToSelect, int howManyChildren)
        {
            int deadPixelIndex = pixelsToSelect;
            foreach (var pixelPair in _pixelPairs)
            {
                for (int i = 0; i < howManyChildren; i++)
                {
                    var newPixel = new Pixel()
                    {
                        Blue = Average(pixelPair.First.Blue, pixelPair.Second.Blue),
                        Green = Average(pixelPair.First.Green, pixelPair.Second.Green),
                        Red = Average(pixelPair.First.Red, pixelPair.Second.Red),
                        Alpha = byte.MaxValue,
                        IndexColumn = pixelContainer.Pixels[deadPixelIndex].IndexColumn,
                        IndexRow = pixelContainer.Pixels[deadPixelIndex].IndexRow,
                        IndexGlobal = pixelContainer.Pixels[deadPixelIndex].IndexGlobal,
                        RankingPoints = 0,
                    };
                    pixelContainer.Pixels[deadPixelIndex] = newPixel;
                    deadPixelIndex++;
                }
            }
            pixelContainer.OrderAscending();
            return;
        }

        private int Average(int first, int second)
        {
            return (int)((first + second) / 2);
        }
    }
}
