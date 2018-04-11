using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Helper.Conversion;
using ColorVisualisation.Model.Helper.Generator;
using System;
using System.Collections;

namespace ColorVisualisation.Model.Crossing
{
    class BitCrossing : BaseCrossing
    {
        protected override void PixelCrossing(PixelCollection pixelCollection, int pixelsToSelect, int howManyChildren)
        {
            var generator = new Random();
            int deadPixelIndex = pixelsToSelect;
            lock (pixelCollection)
            {
                foreach (var pixelPair in _pixelPairs)
                {
                    var firstPixel = pixelPair.First;
                    var secondPixel = pixelPair.Second;


                    var blueBitsFirst = NumberConverter.ToBitArray(firstPixel.Blue);
                    var blueBitsSecond = NumberConverter.ToBitArray(secondPixel.Blue);
                    CrossPixels(ref blueBitsFirst, ref blueBitsSecond);

                    var redBitsFirst = NumberConverter.ToBitArray(firstPixel.Red);
                    var redBitsSecond = NumberConverter.ToBitArray(secondPixel.Red);
                    CrossPixels(ref redBitsFirst, ref redBitsSecond);

                    var greenBitsFirst = NumberConverter.ToBitArray(firstPixel.Green);
                    var greenBitsSecond = NumberConverter.ToBitArray(secondPixel.Green);
                    CrossPixels(ref greenBitsFirst, ref greenBitsSecond);

                    pixelCollection[deadPixelIndex].Blue = NumberConverter.ToInt(blueBitsFirst);
                    pixelCollection[deadPixelIndex].Red = NumberConverter.ToInt(redBitsFirst);
                    pixelCollection[deadPixelIndex].Green = NumberConverter.ToInt(greenBitsFirst);
                    pixelCollection[deadPixelIndex + 1].Blue = NumberConverter.ToInt(blueBitsSecond);
                    pixelCollection[deadPixelIndex + 1].Red = NumberConverter.ToInt(redBitsSecond);
                    pixelCollection[deadPixelIndex + 1].Green = NumberConverter.ToInt(greenBitsSecond);
                    deadPixelIndex += howManyChildren;
                }
            }            
        }

        private void CrossPixels(ref BitArray firstParent, ref BitArray secondParent)
        {
            var firstChild = new BitArray(8);
            var secondChild = new BitArray(8);
            int onesCount = new Random().Next(0, 8);
            var crossArray = BitArrayGenerator.GenerateBitArray(8, onesCount);
            for (int i = 0; i < crossArray.Length; i++)
            {
                if (crossArray[i] == true)
                {
                    firstChild[i] = firstParent[i];
                    secondChild[i] = secondParent[i];
                }
                else
                {
                    firstChild[i] = secondParent[i];
                    secondChild[i] = firstParent[i];
                }
            }
            firstParent = firstChild;
            secondParent = secondChild;
        }
    }
}
