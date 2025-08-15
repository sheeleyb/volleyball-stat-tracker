using StatTrackerGlobal.Domain.Stats;
using StatTrackerGlobal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Domain
{
    public record DomainStatWrapper
    {
        public Set StatSet { get; set; } = new();

        public AttackingStats AttackingStats { get; set; } = new();
        public BlockingStats BlockingStats { get; set; } = new();
        public PassingStats PassingStats { get; set; } = new();
        public ServeRecieveStats ServeRecieveStats { get; set; } = new();
        public ServingStats ServingStats { get; set; } = new();
    }
}
