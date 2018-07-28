using ColorVisualisation.Model.Crossing;
using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Mutation;
using ColorVisualisation.Model.Scoring;
using ColorVisualisation.Model.Selection;

namespace ColorVisualisation.Model
{
    class GeneticManager
    {
        public PixelCollection PixelCollection { get; set; }
        public BaseCrossing Crossing { get; set; }
        public IScoringTable ScoringTable { get; set; }
        public BaseMutation Mutation { get; set; }        
        public int PixelsToSelect { get; set; }
        public int HowManyChildren { get; set; }
        public double MutationRate { get; set; }
        private BaseSelection Selection { get; set; } = new BaseSelection();


        public PixelCollection NextGeneration()
        {
            Selection.Execute(PixelCollection, ScoringTable, PixelsToSelect);            
            Crossing.Execute(PixelCollection, PixelsToSelect, HowManyChildren);
            Mutation.Execute(PixelCollection, MutationRate);
            return PixelCollection;
        }    
    }
}
