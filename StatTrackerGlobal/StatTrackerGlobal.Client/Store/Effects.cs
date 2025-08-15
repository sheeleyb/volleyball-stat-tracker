using Fluxor;
using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.App;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;

namespace StatTrackerGlobal.Client.Store
{
    public class Effects
    {
        public IApplicationService AppService { get; }
        public NavigationManager NavManager { get; }

        public Effects(IApplicationService appService, NavigationManager navManager)
        {
            AppService = appService;
            NavManager = navManager;
        }
        [EffectMethod]
        public async Task HandleUpdateCurrentGameAction(UpdateCurrentGameAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditUpdateCurrentGameAction(action.TeamAgainst, action.Date);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleUpdateCurrentSetAction(UpdateCurrentSetAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditUpdateCurrentSetAction(action.TeamOne, action.TeamTwo, action.Date, action.Order);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleUpdateTeamViewModelAction(UpdateTeamViewModelAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditUpdateCurrentTeamAction(action.team);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleAddGameSetsAction(AddGameSetsAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditAddGameSetsAction(action.TeamAgainst, action.Date);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleAddPlayerAction(AddPlayerAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditAddPlayerAction(action.FirstName, action.LastName, action.JerseyNumber, action.Height, action.Position);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleDeletePlayerAction(DeletePlayerAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditDeletePlayerAction(action.FirstName, action.LastName);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleAddGameAction(AddGameAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditAddGameAction(action.TeamAgainst, action.Date);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandlerDeleteGameAction(DeleteGameAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditDeleteGameAction(action.TeamAgainst, action.Date);
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleInitializeApplicationAction(InitializeApplicationAction action, IDispatcher dispatcher)
        {
            MockViewState newState = AppService.EditInitializeApplicationAction();
            dispatcher.Dispatch(new UpdateViewStateAction(ViewState.MockToViewState(newState)));
        }
        [EffectMethod]
        public async Task HandleNavigateToTeamViewAction(NavigateToTeamViewAction action, IDispatcher dispatcher)
        {
            NavManager.NavigateTo("/teamview/" + action.TeamName);
        }
    }
}
