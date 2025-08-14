using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Domain.Stats
{
    public class BlockingStats
    {
        public int KillBlocks { get; set; }
        public int Touches { get; set; }
        public int BlockErrors { get; set; }
        public double TouchPercent { get; set; }
    }
}
