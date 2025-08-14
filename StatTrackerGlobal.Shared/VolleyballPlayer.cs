using StatTrackerGlobal.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Shared
{
    public record VolleyballPlayer
    {
        public VolleyballPlayer(string firstName, string lastName, int jerseyNumber, string height, string position)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.JerseyNumber = jerseyNumber;
            this.Height = height;
            this.Position = position;
        }
        public VolleyballPlayer()
        {
            FirstName = "";
            LastName = "";
            Team = "";
            JerseyNumber = 0;
            Height = "";
            Position = "";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public int JerseyNumber { get; set; }
        public string Height { get; set; }
        public string Position { get; set; }

        public ImmutableList<DomainStatWrapper> PlayerStats { get; set; } = [];
    }
}
