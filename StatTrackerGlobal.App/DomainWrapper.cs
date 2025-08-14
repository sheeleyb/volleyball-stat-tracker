using StatTrackerGlobal.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App
{
    public record DomainWrapper
    {
        public Team Team { get; set; } = new Team();
        public Game CurrentGame { get; set; } = new Game();
        public Set CurrentSet { get; set; } = new Set();
        public ImmutableList<Game> Games { get; set; } = [];
        public ImmutableList<Set> Sets { get; set; } = [];
        public ImmutableList<VolleyballPlayer> Players { get; set; } = [];
    }
}
