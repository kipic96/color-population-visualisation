using ColorVisualisation.Model.Entity;

namespace ColorVisualisation.Model.Crossing
{
    class AverageCrossing : BaseCrossing
    {
        protected override void PixelCrossing(PixelCollection pixelCollection, int pixelsToSelect, int howManyChildren)
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
                        IndexColumn = pixelCollection[deadPixelIndex].IndexColumn,
                        IndexRow = pixelCollection[deadPixelIndex].IndexRow,
                        IndexGlobal = pixelCollection[deadPixelIndex].IndexGlobal,
                        RankingPoints = 0,
                    };
                    pixelCollection[deadPixelIndex] = newPixel;
                    deadPixelIndex++;
                }
            }
            pixelCollection.OrderAscending();
            return;
        }

        private int Average(int first, int second)
        {
            return (int)((first + second) / 2);
        }
    }
}
