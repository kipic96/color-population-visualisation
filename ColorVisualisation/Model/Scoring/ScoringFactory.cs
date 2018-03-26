using ColorVisualisation.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVisualisation.Model.Scoring
{
    class ScoringFactory
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
