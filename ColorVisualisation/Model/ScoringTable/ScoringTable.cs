using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVisualisation.Model.ScoringTable
{
    interface IScoringTable
    {
        int GetScore(int allPosiblePlaces, int place);
    }
}
