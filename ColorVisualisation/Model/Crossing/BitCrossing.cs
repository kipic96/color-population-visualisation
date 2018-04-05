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
            foreach (var pixelPair in _pixelPairs)
            {
                var firstPixel = pixelPair.First;
                var secondPixel = pixelPair.Second;                
                

                var blueBitsFirst = NumberConversion.ToBitArray(firstPixel.Blue);
                var blueBitsSecond = NumberConversion.ToBitArray(secondPixel.Blue);
                CrossPixels(ref blueBitsFirst, ref blueBitsSecond);                
                
                var redBitsFirst = NumberConversion.ToBitArray(firstPixel.Red);
                var redBitsSecond = NumberConversion.ToBitArray(secondPixel.Red);
                CrossPixels(ref redBitsFirst, ref redBitsSecond);

                var greenBitsFirst = NumberConversion.ToBitArray(firstPixel.Green);
                var greenBitsSecond = NumberConversion.ToBitArray(secondPixel.Green);
                CrossPixels(ref greenBitsFirst, ref greenBitsSecond);

                pixelCollection[deadPixelIndex].Blue = NumberConversion.ToInt(blueBitsFirst);
                pixelCollection[deadPixelIndex].Red = NumberConversion.ToInt(redBitsFirst);
                pixelCollection[deadPixelIndex].Green = NumberConversion.ToInt(greenBitsFirst);
                pixelCollection[deadPixelIndex + 1].Blue = NumberConversion.ToInt(blueBitsSecond);
                pixelCollection[deadPixelIndex + 1].Red = NumberConversion.ToInt(redBitsSecond);
                pixelCollection[deadPixelIndex + 1].Green = NumberConversion.ToInt(greenBitsSecond);
                deadPixelIndex += howManyChildren;
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
