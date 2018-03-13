using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public GeneticManager(PixelContainer pixelContainer)
        {
            _pixelContainer = pixelContainer;
             _pixelsToSelect = new NumberConversion().ToEvenNumber
                ((int)(_pixelContainer.Width * _pixelContainer.Height
                    / (_valuesInPixel * _pixelsToSelectRatio)));
        }

        private PixelContainer _pixelContainer;
        private Selection _selection;
        private CrossoverStyle _crossover;

        private int _howManyChildren = int.Parse(Resources.ChildrenCount);
        private int _pixelsToSelect;
        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);
        private double _pixelsToSelectRatio = double.Parse(Resources.PixelsToSelectRatio);

        public PixelContainer NextGeneration()
        {
            _selection = new Selection(_pixelContainer, _pixelsToSelect);
            _selection.Execute();
            _crossover = new CrossoverByAverage(_pixelContainer, _pixelsToSelect, _howManyChildren);
            _crossover.Execute();
            _pixelContainer.Shuffle();
            return _pixelContainer;
        }    
    }
}
