using ColorVisualisation.Model.Entity;

namespace ColorVisualisation.Model.Mutation
{
    abstract class BaseMutation
    {
        protected const int probabilityFixer = 1000;
        protected const int mutationStrengh = 10;

        public abstract void Execute(PixelCollection pixelCollection, double mutationRate);
    }    
}
