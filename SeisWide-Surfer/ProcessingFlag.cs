using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeisWide_Surfer
{
    [Flags]
    public enum ProcessingFlag
    {
        None = 0,
        Bind = 1,
        Project =           1 << 1,
        SplitHodographs =   1 << 2,
        Interpolate =       1 << 3,
        SquaredTimes =      1 << 4,
        MeanVelocities =    1 << 5, 
        ShowPyramid =       1 << 6,
        DepthsOnXCenters =  1 << 7
    }
}
