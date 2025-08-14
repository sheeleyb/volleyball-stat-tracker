using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StatTrackerGlobal.App.ViewModels.GameOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.TeamOverviewViewModel;

namespace StatTrackerGlobal.App.Interfaces
{
    public interface IApplicationService
    {
        public MockViewState EditUpdateCurrentGameAction(string TeamAgainst, DateTime Date);
        public MockViewState EditUpdateCurrentTeamAction(TeamOverviewViewModel Team);
        public MockViewState EditAddGameSetsAction(string TeamAgainst, DateTime Date);
        public MockViewState EditAddPlayerAction(string FirstName, string LastName, int JerseyNumber, string Height, string Position);
        public MockViewState EditDeletePlayerAction(string FirstName, string LastName);
        public MockViewState EditAddGameAction(string TeamAgainst, DateTime Date);
        public MockViewState EditDeleteGameAction(string TeamAgainst, DateTime Date);
        public MockViewState EditInitializeApplicationAction();
    }
}
