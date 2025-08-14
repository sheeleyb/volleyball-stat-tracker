using Microsoft.AspNetCore.Components;
using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.Data;

namespace StatTrackerGlobal.Test
{
    public class Tests
    {

        [Test]
        public void SaveTest()
        {
            var SetGuid1 = Guid.NewGuid();
            var SetGuid2 = Guid.NewGuid();
            var SetGuid3 = Guid.NewGuid();
            var TeamGuid1 = Guid.NewGuid();
            var TeamGuid2 = Guid.NewGuid();
            var PlayerGuid  = Guid.NewGuid();
            var GameGuid = Guid.NewGuid();
            StatTrackerGlobalDataWrapper statTrackerGlobalDataWrapper = new()
            {
                GameData = [new GameData()
                {
                    Id = GameGuid,
                    TimeStamp = DateTime.Now,
                    Sets = [SetGuid1, SetGuid2, SetGuid3]
                }],
                SetData = [new SetData()
                {
                    Id = SetGuid1,
                    Order = 1,
                    StatEvents = []
                }, new SetData() 
                {
                    Id = SetGuid2,
                    Order = 2,
                    StatEvents = []
                }, new SetData() 
                {
                    Id = SetGuid3,
                    Order = 3,
                    StatEvents = []
                }],
                PlayerData = [new PlayerData() 
                {
                    Id = PlayerGuid,
                    TeamId = TeamGuid1,
                }],
                TeamData = new TeamData()
                {
                    Name = "Test",
                    Id = TeamGuid1
                }

            };
            JsonPersistenceService persistence = new JsonPersistenceService();
            persistence.Save(statTrackerGlobalDataWrapper);
        }

        [Test]
        public void LoadTest()
        {
            JsonPersistenceService persistence = new JsonPersistenceService();
            StatTrackerGlobalDataWrapper data = persistence.Load();

            Assert.AreEqual(data.PlayerData.ElementAt(0).TeamId, data.TeamData.Id);
        }
    }
}