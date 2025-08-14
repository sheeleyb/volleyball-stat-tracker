using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App.ViewModels
{
    public class GameOverviewViewModel
    {
        public string TeamAgainst { get; init; } = string.Empty;
        public DateTime Date { get; init; }
        public record GameOverviewPlayer(string FirstName, string LastName, int JerseyNumber, string Height, string Position, bool ShowPlayerDetails);
        public ImmutableList<GameOverviewPlayer> Players { get; init; } = [];
        public record GameOverviewSet(ImmutableList<GameOverviewPlayer> Players, int Order);
        public ImmutableList<GameOverviewSet> Sets { get; init; } = [];
    }
}
