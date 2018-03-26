using ColorVisualisation.Properties;
using ColorVisualisation.Model.Scoring;
using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Helper.Conversion;
using ColorVisualisation.Model.Crossing;
using ColorVisualisation.Model.Selection;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public PixelCollection PixelCollection { get; set; }        
        public BaseCrossing Crossing { get; set; }
        public IScoringTable ScoringTable { get; set; }
        public BaseSelection Selection { get; set; }
        public int PixelsToSelect { get; set; }

        public int HowManyChildren = int.Parse(Resources.ChildrenCount);
        

        public PixelCollection NextGeneration()
        {
            Selection = new BaseSelection(PixelCollection, PixelsToSelect);
            Crossing = new CrossingByAverage(PixelCollection, PixelsToSelect, HowManyChildren);
            Selection.Execute();            
            Crossing.Execute();
            return PixelCollection;
        }    
    }
}
