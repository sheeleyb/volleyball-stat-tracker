using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App.ViewModels
{
    public class TeamOverviewViewModel
    {
        public string TeamName { get; init; } = string.Empty;
        public record TeamOverviewPlayer(string FirstName, string LastName, int JerseyNumber, string Height, string Position);
        public ImmutableList<TeamOverviewPlayer> Players { get; init; } = [];
        public record TeamOverviewGame(string Against, string Winner, string Score, DateTime Date);
        public ImmutableList<TeamOverviewGame> Games { get; init; } = [];

    }
}
