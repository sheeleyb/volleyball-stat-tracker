using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.App;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Client.Store;
using System.Collections.Immutable;

namespace StatTrackerGlobal.Client.Pages
{
    public partial class SetView
    {
        [Parameter]
        public string TeamName { get; set; }
        public TeamOverviewViewModel Team
        {
            get => ViewState.Value.TeamViewModel;
            set => Dispatcher.Dispatch(new UpdateTeamViewModelAction(value));
        }
        public SetOverviewViewModel ViewModel
        {
            get => ViewState.Value.SetViewModel;
        }
    }
}
