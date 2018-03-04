using ColorVisualisation.Model.Generator;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public GeneticManager(byte[,,] rawPixels)
        {
            _rawPixels = rawPixels;
        }

        private byte[,,] _rawPixels;
        
        public byte[,,] NextGeneration()
        {
            var gen = new BitmapGenerator();
            _rawPixels = gen.Generate();
            return _rawPixels;
        }    
    }
}
