using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App.DataModels
{
    public class StatTrackerGlobalDataWrapper
    {
        public ImmutableList<GameData> GameData { get; set; } = [];
        public ImmutableList<SetData> SetData { get; set; } = [];
        public ImmutableList<PlayerData> PlayerData { get; set; } = [];
        public TeamData TeamData { get; set; }
    }
}
