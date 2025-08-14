using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Domain.Stats
{
    public class PassingStats
    {
        public int Digs { get; set; }
        public int BallTouches { get; set; }
        public int BallMisses { get; set; }
        public double TouchPercent { get; set; }
    }
}
