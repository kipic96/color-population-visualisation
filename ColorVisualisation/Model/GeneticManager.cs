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
            var gen = new BitmapGenerator(_pixelContainer.Width, _pixelContainer.Height);
            _pixelContainer = new PixelContainer(gen.Generate(), 
                _pixelContainer.Width, _pixelContainer.Height);
            var sel = new Selection(_pixelContainer);
            sel.Execute();
            return null; //TODO make a conversion from PixelContainer to _rawPixels;
        }    
    }
}
