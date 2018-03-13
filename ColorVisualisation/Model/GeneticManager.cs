using ColorVisualisation.Model.Generator;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public GeneticManager(PixelContainer pixelContainer)
        {
            _pixelContainer = pixelContainer;
        }

        private PixelContainer _pixelContainer;

        public PixelContainer NextGeneration()
        {
            var sel = new Selection(_pixelContainer);
            sel.Execute();
            return _pixelContainer; //TODO make a conversion from PixelContainer to _rawPixels;
        }    
    }
}
