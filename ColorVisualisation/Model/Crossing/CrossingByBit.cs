using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorVisualisation.Model.Entity;

namespace ColorVisualisation.Model.Crossing
{
    class CrossingByBit : BaseCrossing
    {
        public CrossingByBit(PixelCollection pixelCollection, int pixelsToSelect, int howManyChildren) 
            : base(pixelCollection, pixelsToSelect, howManyChildren)
        {
        }

        public override PixelCollection Execute()
        {
            throw new NotImplementedException();
        }
    }
}
