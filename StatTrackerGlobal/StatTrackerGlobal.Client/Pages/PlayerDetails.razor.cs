using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.Client.Store;
using static StatTrackerGlobal.App.ViewModels.SetOverviewViewModel;

namespace StatTrackerGlobal.Client.Pages
{
    public partial class PlayerDetails
    {

        [Parameter]
        public SetOverviewPlayer SetPlayer { get; set; }

        public bool isShowing { get; set; } = false;

        public void Open()
        {
            isShowing = true;
        }
        public void Close()
        {
            isShowing = false;
        }

        public void IncrementKills()
        {
            int temp = SetPlayer.AttackStats.Kills;
            SetOverviewAttacks newAttacks = SetPlayer.AttackStats with { Kills = temp + 1 };
            SetPlayer = SetPlayer with { AttackStats = newAttacks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementKills()
        {
            int temp = SetPlayer.AttackStats.Kills;
            SetOverviewAttacks newAttacks = SetPlayer.AttackStats with { Kills = temp - 1 };
            SetPlayer = SetPlayer with { AttackStats = newAttacks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementAttempts()
        {
            int temp = SetPlayer.AttackStats.Attempts;
            SetOverviewAttacks newAttacks = SetPlayer.AttackStats with { Attempts = temp + 1 };
            SetPlayer = SetPlayer with { AttackStats = newAttacks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementAttempts()
        {
            int temp = SetPlayer.AttackStats.Attempts;
            SetOverviewAttacks newAttacks = SetPlayer.AttackStats with { Attempts = temp - 1 };
            SetPlayer = SetPlayer with { AttackStats = newAttacks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementErrors()
        {
            int temp = SetPlayer.AttackStats.Errors;
            SetOverviewAttacks newAttacks = SetPlayer.AttackStats with { Errors = temp + 1 };
            SetPlayer = SetPlayer with { AttackStats = newAttacks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementErrors()
        {
            int temp = SetPlayer.AttackStats.Errors;
            SetOverviewAttacks newAttacks = SetPlayer.AttackStats with { Errors = temp - 1 };
            SetPlayer = SetPlayer with { AttackStats = newAttacks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementKillBlocks()
        {
            int temp = SetPlayer.BlockStats.KillBlocks;
            SetOverviewBlocks newBlocks = SetPlayer.BlockStats with { KillBlocks = temp + 1 };
            SetPlayer = SetPlayer with { BlockStats = newBlocks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementKillBlocks()
        {
            int temp = SetPlayer.BlockStats.KillBlocks;
            SetOverviewBlocks newBlocks = SetPlayer.BlockStats with { KillBlocks = temp - 1 };
            SetPlayer = SetPlayer with { BlockStats = newBlocks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementTouches()
        {
            int temp = SetPlayer.BlockStats.Touches;
            SetOverviewBlocks newBlocks = SetPlayer.BlockStats with { Touches = temp + 1 };
            SetPlayer = SetPlayer with { BlockStats = newBlocks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementTouches()
        {
            int temp = SetPlayer.BlockStats.Touches;
            SetOverviewBlocks newBlocks = SetPlayer.BlockStats with { Touches = temp - 1 };
            SetPlayer = SetPlayer with { BlockStats = newBlocks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementBlockErrors()
        {
            int temp = SetPlayer.BlockStats.BlockErrors;
            SetOverviewBlocks newBlocks = SetPlayer.BlockStats with { BlockErrors = temp + 1 };
            SetPlayer = SetPlayer with { BlockStats = newBlocks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementBlockErrors()
        {
            int temp = SetPlayer.BlockStats.BlockErrors;
            SetOverviewBlocks newBlocks = SetPlayer.BlockStats with { BlockErrors = temp - 1 };
            SetPlayer = SetPlayer with { BlockStats = newBlocks };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementDigs()
        {
            int temp = SetPlayer.PassStats.Digs;
            SetOverviewPasses newPasses = SetPlayer.PassStats with { Digs = temp + 1 };
            SetPlayer = SetPlayer with { PassStats = newPasses };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementDigs()
        {
            int temp = SetPlayer.PassStats.Digs;
            SetOverviewPasses newPasses = SetPlayer.PassStats with { Digs = temp - 1 };
            SetPlayer = SetPlayer with { PassStats = newPasses };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementBallTouches()
        {
            int temp = SetPlayer.PassStats.BallTouches;
            SetOverviewPasses newPasses = SetPlayer.PassStats with { BallTouches = temp + 1 };
            SetPlayer = SetPlayer with { PassStats = newPasses };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementBallTouches()
        {
            int temp = SetPlayer.PassStats.BallTouches;
            SetOverviewPasses newPasses = SetPlayer.PassStats with { BallTouches = temp - 1 };
            SetPlayer = SetPlayer with { PassStats = newPasses };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementBallMisses()
        {
            int temp = SetPlayer.PassStats.BallMisses;
            SetOverviewPasses newPasses = SetPlayer.PassStats with { BallMisses = temp + 1 };
            SetPlayer = SetPlayer with { PassStats = newPasses };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementBallMisses()
        {
            int temp = SetPlayer.PassStats.BallMisses;
            SetOverviewPasses newPasses = SetPlayer.PassStats with { BallMisses = temp - 1 };
            SetPlayer = SetPlayer with { PassStats = newPasses };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementAces()
        {
            int temp = SetPlayer.ServeStats.Aces;
            SetOverviewServe newServes = SetPlayer.ServeStats with { Aces = temp + 1 };
            SetPlayer = SetPlayer with { ServeStats = newServes };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementAces()
        {
            int temp = SetPlayer.ServeStats.Aces;
            SetOverviewServe newServes = SetPlayer.ServeStats with { Aces = temp - 1 };
            SetPlayer = SetPlayer with { ServeStats = newServes };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementServesMade()
        {
            int temp = SetPlayer.ServeStats.ServesMade;
            SetOverviewServe newServes = SetPlayer.ServeStats with { ServesMade = temp + 1 };
            SetPlayer = SetPlayer with { ServeStats = newServes };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementServesMade()
        {
            int temp = SetPlayer.ServeStats.ServesMade;
            SetOverviewServe newServes = SetPlayer.ServeStats with { ServesMade = temp - 1 };
            SetPlayer = SetPlayer with { ServeStats = newServes };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void IncrementServesMissed()
        {
            int temp = SetPlayer.ServeStats.ServesMissed;
            SetOverviewServe newServes = SetPlayer.ServeStats with { ServesMissed = temp + 1 };
            SetPlayer = SetPlayer with { ServeStats = newServes };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
        public void DecrementServesMissed()
        {
            int temp = SetPlayer.ServeStats.ServesMissed;
            SetOverviewServe newServes = SetPlayer.ServeStats with { ServesMissed = temp - 1 };
            SetPlayer = SetPlayer with { ServeStats = newServes };
            Dispatcher.Dispatch(new UpdatePlayerStatAction(SetPlayer, ViewState.Value.SetViewModel));
        }
    }
}
