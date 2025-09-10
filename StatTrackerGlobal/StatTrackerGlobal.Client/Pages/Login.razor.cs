using Fluxor;
using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Client.Store;
using StatTrackerGlobal.Shared;

namespace StatTrackerGlobal.Client.Pages
{
    public partial class Login
    {
        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Inject]
        IState<ViewState> ViewState { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }
        [Inject]
        IApplicationService AppService { get; set; }

        private string Username { get; set; }
        private string Password { get; set; }
        private string TeamName { get; set; }
        private string SelectedFilePath { get; set; }
        public TeamOverviewViewModel TeamViewModel
        {
            get => ViewState.Value.TeamViewModel;
            set => Dispatcher.Dispatch(new UpdateTeamViewModelAction(value));
        }

        public void JustGoToAPlace()
        {
            TeamOverviewViewModel exampleTeam = new TeamOverviewViewModel()
            {
                TeamName = "Grinnell"
            };
            //Dispatcher.Dispatch(new NavigateToTeamViewAction(exampleTeam.TeamName));
            NavManager.NavigateTo("/teamview/" + TeamViewModel.TeamName);
        }
    }
}
