using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Domain.Stats
{
    public class AttackingStats
    {
        public int Kills { get; set; }
        public int Attempts { get; set; }
        public int Errors { get; set; }
        public double KillPercentage { get; set; }
        public double ErrorPercentage { get; set; }
        public double KillErrorRatio { get; set; }
    }
}
