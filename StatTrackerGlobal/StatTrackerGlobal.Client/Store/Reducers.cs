using Fluxor;
using StatTrackerGlobal.Shared;
using System.Collections.Immutable;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.App.Interfaces;

namespace StatTrackerGlobal.Client.Store
{
    public static class Reducers
    {
        [ReducerMethod]
        public static ViewState ReduceUpdateViewStateAction(ViewState state, UpdateViewStateAction action)
        {
            return action.ViewState as ViewState;
        }
    }
}
