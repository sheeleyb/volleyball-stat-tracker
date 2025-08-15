using StatTrackerGlobal.App;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Client.Store;
using System.Collections.Immutable;

namespace StatTrackerGlobal.Client.Pages
{
    public partial class SetView
    {
        //public IApplicationService AppService = new MockApplicationService();
        //public ImmutableList<PlayerViewModel> Players
        //{
        //    get => AppService.FetchPlayers(Team);
        //    set => AppService.SavePlayers(value, Team);
        //}
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
