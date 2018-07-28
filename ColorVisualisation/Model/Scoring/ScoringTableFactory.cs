using ColorVisualisation.Properties;
using System;

namespace ColorVisualisation.Model.Scoring
{
    class ScoringTableFactory
    {
        public static IScoringTable Create(string scoringType)
        {
            if (scoringType == Resources.LinearScoring)
                return new LinearScoringTable();
            if (scoringType == Resources.AdjustedScoring)
                return new AdjustedScoringTable();
            throw new ArgumentException(Resources.ErrorScoringType);
        }
    }
}
