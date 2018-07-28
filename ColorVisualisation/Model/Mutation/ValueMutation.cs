using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Helper.Extension;
using System;

namespace ColorVisualisation.Model.Mutation
{
    class ValueMutation : BaseMutation
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
                        pixel.Blue += generator.Next(-mutationStrengh, mutationStrengh);
                        pixel.Red += generator.Next(-mutationStrengh, mutationStrengh);
                        pixel.Green += generator.Next(-mutationStrengh, mutationStrengh);
                        pixel.Blue = Math.Min(pixel.Blue, byte.MaxValue);
                        pixel.Red = Math.Min(pixel.Red, byte.MaxValue);
                        pixel.Green = Math.Min(pixel.Green, byte.MaxValue);
                        pixel.Blue = Math.Max(pixel.Blue, 0);
                        pixel.Red = Math.Max(pixel.Red, 0);
                        pixel.Green = Math.Max(pixel.Green, 0);
                    }
                }
            }
        }
    }
}
