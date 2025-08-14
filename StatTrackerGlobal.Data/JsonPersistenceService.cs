using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.Interfaces;
using System.Text.Json;

namespace StatTrackerGlobal.Data
{
    public class JsonPersistenceService : IPersistence
    {
        const string DATABASE_PATH = @"c:/StatTrackerGlobalDatabase/test.json";
        public StatTrackerGlobalDataWrapper Load()
        {
            string jsonString = string.Empty;
            jsonString = File.ReadAllText(DATABASE_PATH);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false,
                WriteIndented = true
            };
            int x = 20;
            StatTrackerGlobalDataWrapper? data = JsonSerializer.Deserialize<StatTrackerGlobalDataWrapper>(jsonString, options);
            return data;
        }

        public void Save(StatTrackerGlobalDataWrapper value)
        {
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(value, jsonOptions);
            File.WriteAllText(DATABASE_PATH, jsonString);
        }
    }
}
