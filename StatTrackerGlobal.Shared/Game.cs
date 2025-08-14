using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Shared
{
    public record Game
    {
        public Game(string teamOne, string teamTwo)
        {
            Sets = Sets.Add(new Set(teamOne, teamTwo));
            Date = DateTime.Now;
            TeamOne = teamOne;
            TeamTwo = teamTwo;
        }
        public Game()
        {
            Date = DateTime.Now;
        }
        public DateTime Date { get; set; }
        public ImmutableList<Set> Sets { get; set; } = ImmutableList<Set>.Empty;
        public string TeamOne { get; set; } = string.Empty;
        public string TeamTwo { get; set; } = string.Empty;
        public int TeamOneSetsWon
        {
            get => CalculateSetsWon(TeamOne);
        }
        public int TeamTwoSetsWon
        {
            get => CalculateSetsWon(TeamTwo);
        }
        public string Score
        {
            get => TeamOneSetsWon + "-" + TeamTwoSetsWon;
        }
        public string Winner
        {
            get => (TeamOneSetsWon > TeamTwoSetsWon) ? TeamOne : TeamTwo;
        }

        public int CalculateSetsWon(string teamName)
        {
            int SetsWon = 0;
            foreach (Set set in Sets)
            {
                if (set.Winner == teamName)
                    SetsWon++;
            }
            return SetsWon;
        }
    }
}
