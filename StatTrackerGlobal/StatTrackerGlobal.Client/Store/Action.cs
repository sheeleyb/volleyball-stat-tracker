using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.Shared;
using System.Collections.Immutable;
using Fluxor.Blazor.Web.Middlewares.Routing;
using static StatTrackerGlobal.App.ViewModels.TeamOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.GameOverviewViewModel;

namespace StatTrackerGlobal.Client.Store
{
    public record UpdateViewStateAction(ViewState ViewState);
    public record UpdateCurrentGameAction(string TeamAgainst, DateTime Date);
    public record UpdateCurrentSetAction(string TeamOne, string TeamTwo, DateTime Date, int Order);
    public record UpdateTeamViewModelAction(TeamOverviewViewModel team);
    public record AddGameAction(string TeamAgainst, DateTime Date);
    public record AddGameSetsAction(string TeamAgainst, DateTime Date);
    public record DeleteGameAction(string TeamAgainst, DateTime Date);
    public record AddPlayerAction(string FirstName, string LastName, int JerseyNumber, string Height, string Position);
    public record DeletePlayerAction(string FirstName, string LastName);
    public record InitializeApplicationAction();
    public record NavigateToTeamViewAction(string TeamName);
}
