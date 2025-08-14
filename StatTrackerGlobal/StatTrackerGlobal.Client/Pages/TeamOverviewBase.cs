using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.Shared;
using System.Collections.Immutable;
using StatTrackerGlobal.Client.Store;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using StatTrackerGlobal.App.DataModels;
using static StatTrackerGlobal.App.ViewModels.TeamOverviewViewModel;

namespace StatTrackerGlobal.Client.Pages
{
    public class TeamOverviewBase : FluxorComponent
    {
        [Parameter]
        public string TeamName { get; set; }
        [Inject]
        IState<ViewState> ViewState { get; set; }
        [Inject]
        IDispatcher Dispatcher { get; set; }
        [Inject]
        IApplicationService AppService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public TeamOverviewViewModel ViewModel 
        {
            get => ViewState.Value.TeamViewModel;
            set => Dispatcher.Dispatch(new UpdateTeamViewModelAction(value));
        }
        public ImmutableList<TeamOverviewPlayer> Players
        {
            get => ViewModel.Players;
        }
        public ImmutableList<TeamOverviewGame> Games
        {
            get => ViewModel.Games;
        }
        public GameOverviewViewModel GameViewModel
        {
            get => ViewState.Value.GameViewModel;
        }
        public void TeamViewToGameView(TeamOverviewGame gameToNav)
        {
            Dispatcher.Dispatch(new UpdateCurrentGameAction(gameToNav.Against, gameToNav.Date));
            NavigationManager.NavigateTo("teamview/" + TeamName + "/gameview");
        }
    }
}
