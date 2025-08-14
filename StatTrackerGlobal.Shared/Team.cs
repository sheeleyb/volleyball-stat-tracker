using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Shared
{
    public record Team
    {
        public string Name { get; set; }
        public TeamCredentials Credentials { get; set; }
        public ImmutableList<Game> Games { get; set; }
        public ImmutableList<VolleyballPlayer> Players { get; set; }

        public Team(string name)
        {
            Name = name;
            Games = ImmutableList<Game>.Empty;
        }
        public Team()
        {
            Name = "";
            Credentials = new TeamCredentials();
            Games = ImmutableList<Game>.Empty;
        }
    }
}
