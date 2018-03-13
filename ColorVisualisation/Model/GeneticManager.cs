using ColorVisualisation.Model.Generator;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public GeneticManager(byte[,,] rawPixels, int width, int height)
        {
            _pixelContainer = new PixelContainer(rawPixels, width, height);
        }

        private PixelContainer _pixelContainer;

        public byte[,,] NextGeneration()
        {
            var sel = new Selection(_pixelContainer);
            sel.Execute();
            return null; //TODO make a conversion from PixelContainer to _rawPixels;
        }    
    }
}
