using ColorVisualisation.Properties;
using ColorVisualisation.Model.Scoring;
using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Crossing;
using ColorVisualisation.Model.Selection;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public PixelCollection PixelCollection { get; set; }
        public BaseCrossing Crossing { get; set; }
        public IScoringTable ScoringTable { get; set; }
        public BaseSelection Selection { get; set; } = new BaseSelection();
        public int PixelsToSelect { get; set; }
        public int HowManyChildren;        

        public PixelCollection NextGeneration()
        {
            Selection.Execute(PixelCollection, ScoringTable, PixelsToSelect);            
            Crossing.Execute(PixelCollection, PixelsToSelect, HowManyChildren);
            return PixelCollection;
        }    
    }
}
