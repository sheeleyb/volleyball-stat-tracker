using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.Shared;

namespace StatTrackerGlobal.Client.Pages
{
    public class PlayerDetailsBase : FluxorComponent
    {
        [Parameter]
        public VolleyballPlayer Player { get; set; }

        public void Increment(ref int stat)
        {
            stat++;
        }
        public void Decrement(ref int stat)
        {
            if (stat > 0)
            {
                stat--;
            }
        }
    }
}
