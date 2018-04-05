using ColorVisualisation.Model.Entity;
using System;

namespace ColorVisualisation.Model.Mutation
{
    abstract class BaseMutation
    {
        protected const int probabilityFixer = 1000;
        protected const int mutationStrengh = 10;

        public abstract void Execute(PixelCollection pixelCollection, double mutationRate);

        protected bool WillMutationHappen(double mutationRate)
        {
            double probability = mutationRate / probabilityFixer;
            if (probability > 1 || probability < 0)
            {
                throw new ArgumentException("Probability must be from range <0,1>");
            }
            var generator = new Random();
            if (probability >= generator.NextDouble())
                return true;
            return false;
        }
    }    
}
