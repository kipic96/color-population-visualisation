using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVisualisation.Model.ScoringTable
{
    class LinearScoringTable : IScoringTable
    {
        public int GetScore(int allPosiblePlaces, int place)
        {
            return allPosiblePlaces - place;
        }
    }
}
