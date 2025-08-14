using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App.DataModels
{
    public record StatEventDatum(Guid PlayerId, StatEvent StatEvent);
}
