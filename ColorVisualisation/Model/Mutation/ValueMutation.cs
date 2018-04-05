using ColorVisualisation.Model.Entity;
using System;

namespace ColorVisualisation.Model.Mutation
{
    class ValueMutation : BaseMutation
    {
        public override void Execute(PixelCollection pixelCollection, double mutationRate)
        {
            var generator = new Random();
            foreach (var pixel in pixelCollection)
            {
                if (WillMutationHappen(mutationRate))
                {
                    pixel.Blue += generator.Next(-mutationStrengh, mutationStrengh);                    
                    pixel.Red += generator.Next(-mutationStrengh, mutationStrengh);
                    pixel.Green += generator.Next(-mutationStrengh, mutationStrengh);
                    pixel.Blue = Math.Min(pixel.Blue, byte.MaxValue);
                    pixel.Red = Math.Min(pixel.Red, byte.MaxValue);
                    pixel.Green = Math.Min(pixel.Green, byte.MaxValue);
                }
            }
        }
    }
}
