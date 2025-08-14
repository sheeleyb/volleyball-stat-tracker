using StatTrackerGlobal.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Shared
{
    public record Set
    {
        public Set()
        {
            TeamOne = "";
            TeamTwo = "";
        }
        public Set(string teamOne, string teamTwo)
        {
            TeamOne = teamOne;
            TeamTwo = teamTwo;
        }
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }
        public string Winner
        {
            get => (TeamOneScore >= TeamTwoScore) ? TeamOne : TeamTwo;
        }
        public int TeamOneScore { get; set; }
        public int TeamTwoScore { get; set; }
        public int Order { get; set; }
        public ImmutableList<VolleyballPlayer> LocalPlayers { get; set; } = ImmutableList<VolleyballPlayer>.Empty;

    }
}
