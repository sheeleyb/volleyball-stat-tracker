using Fluxor;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Shared;
using System.Collections.Immutable;
using StatTrackerGlobal.App.Interfaces;

namespace StatTrackerGlobal.Client.Store
{
    [FeatureState]
    public record ViewState() : MockViewState
    {
        //public ViewState() : this()
        //{
        //}
        //public static ViewState ToViewState(MockViewState mockState)
        //{
        //    return new ViewState(mockState.TeamViewModel, mockState.GameViewModel);
        //}
        public static ViewState MockToViewState(MockViewState mockViewState)
        {
            return new ViewState()
            {
                TeamViewModel = mockViewState.TeamViewModel,
                GameViewModel = mockViewState.GameViewModel,
                SetViewModel = mockViewState.SetViewModel
            };
        }

    }
}

