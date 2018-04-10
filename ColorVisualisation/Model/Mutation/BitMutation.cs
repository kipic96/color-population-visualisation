using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Helper.Conversion;
using ColorVisualisation.Model.Helper.Extension;
using ColorVisualisation.Model.Helper.Generator;
using System;

namespace ColorVisualisation.Model.Mutation
{
    class BitMutation : BaseMutation
    {
        public override void Execute(PixelCollection pixelCollection, double mutationRate)
        {
            var generator = new Random();
            lock (pixelCollection)
            {
                foreach (var pixel in pixelCollection)
                {
                    if (generator.WillEventHappen(mutationRate / probabilityFixer))
                    {
                        var blueBits = NumberConverter.ToBitArray(pixel.Blue);
                        var redBits = NumberConverter.ToBitArray(pixel.Red);
                        var greenBits = NumberConverter.ToBitArray(pixel.Green);
                        var randomBits = BitArrayGenerator.GenerateBitArray(blueBits.Count, (int)(mutationStrengh));
                        blueBits = blueBits.Xor(randomBits);
                        redBits = redBits.Xor(randomBits);
                        greenBits = greenBits.Xor(randomBits);
                        pixel.Blue = NumberConverter.ToInt(blueBits);
                        pixel.Red = NumberConverter.ToInt(redBits);
                        pixel.Green = NumberConverter.ToInt(greenBits);
                    }
                }
            }
        }

        
    }
}
