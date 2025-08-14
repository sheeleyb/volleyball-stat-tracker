using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.Client.Store;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Shared;
using System.Collections.Immutable;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App;
using static StatTrackerGlobal.App.ViewModels.TeamOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.GameOverviewViewModel;
using System.Diagnostics.CodeAnalysis;

namespace StatTrackerGlobal.Client.Pages
{
    public partial class GameView
    {
        [Parameter]
        public string TeamName { get; set; }

        public TeamOverviewViewModel Team
        {
            get => ViewState.Value.TeamViewModel;
            set => Dispatcher.Dispatch(new UpdateTeamViewModelAction(value));
        }
        public ImmutableList<GameOverviewPlayer> Players
        {
            get => ViewModel.Players;
            set => Players = value;
        }
        public GameOverviewViewModel ViewModel
        {
            get => ViewState.Value.GameViewModel;
        }
        public ImmutableList<GameOverviewSet> Sets
        {
            get => ViewModel.Sets;
        }
        public void BackToTeamView()
        {
            NavManager.NavigateTo("teamview/" + TeamName);
        }
        public void AddSet()
        {
            Dispatcher.Dispatch(new AddGameSetsAction(ViewModel.TeamAgainst, ViewModel.Date));
        }
    }
}
