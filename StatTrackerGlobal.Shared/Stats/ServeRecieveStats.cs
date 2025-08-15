using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Domain.Stats
{
    public record ServeRecieveStats
    {
        public int ThreePointPasses { get; set; }
        public int TwoPointPasses { get; set; }
        public int OnePointPasses { get; set; }
        public int ZeroPointPasses { get; set; }
    }
}
