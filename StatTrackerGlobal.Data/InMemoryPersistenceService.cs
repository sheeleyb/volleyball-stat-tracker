using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.Interfaces;

namespace StatTrackerGlobal.Data
{
    // populate example data in constructor for this class.
    public class InMemoryPersistenceService : IPersistence
    {
        private StatTrackerGlobalDataWrapper data { get; set; } = new();
        public StatTrackerGlobalDataWrapper Load()
        {
            Guid TeamId = Guid.NewGuid();
            Guid Playerid = Guid.NewGuid();
            Guid GameId = Guid.NewGuid();
            Guid SetId1 = Guid.NewGuid();
            Guid SetId2 = Guid.NewGuid();
            Guid SetId3 = Guid.NewGuid();
            data.TeamData = new TeamData()
            {
                Name = "Grinnell",
                Id = TeamId
            };
            data.SetData = [new SetData()
            {
                Id = SetId1,
                Order = 1,
                TeamOneScore = 25,
                TeamTwoScore = 20
            }, new SetData() {
                Id = SetId2,
                Order = 2,
                TeamOneScore = 23,
                TeamTwoScore = 25
            }, new SetData() {
                Id = SetId3,
                Order = 3,
                TeamOneScore= 16,
                TeamTwoScore = 14
            }];
            data.GameData = [new GameData()
            {
                Id = GameId,
                TeamTwo = "UNI",
                TimeStamp = DateTime.Now,
                Sets = [SetId1, SetId2, SetId3]
            }];
            data.PlayerData = [new PlayerData()
            {
                FirstName = "Ben",
                LastName = "Sheeley",
                JerseyNumber = 4,
                Height = "6'3\"",
                Position = "Middle Blocker",
                TeamId = TeamId,
                Id = Playerid
            }];
            return data;
        }

        public void Save(StatTrackerGlobalDataWrapper value)
        {
            data = value;
        }
    }
}
