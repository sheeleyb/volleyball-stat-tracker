using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Domain.Stats
{
    public class ServingStats
    {
        public int Aces { get; set; }
        public int ServesMade { get; set; }
        public int TotalServes { get; set; }
        public double ServePercentages { get; set; }
    }
}
