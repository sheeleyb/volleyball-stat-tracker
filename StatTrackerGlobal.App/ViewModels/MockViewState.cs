using Fluxor;
using StatTrackerGlobal.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App.ViewModels
{
    public record MockViewState(TeamOverviewViewModel TeamViewModel,
                                GameOverviewViewModel GameViewModel,
                                SetOverviewViewModel SetViewModel) 
    {
        public MockViewState() : this(new TeamOverviewViewModel(), new GameOverviewViewModel(), new SetOverviewViewModel())
        {
        }
    }
}
