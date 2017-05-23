using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeisWide_Surfer
{
    class SegyHeaderRecord
    {
        public int Trace { get; set; }
        public int CDP { get; set; }
        public int Distance { get; set; }
        public int GroupX { get; set; }
        public int GroupY { get; set; }
    }
}
