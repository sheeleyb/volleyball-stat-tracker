

namespace StatTrackerGlobal.App.DataModels
{
    public record TeamData() : Entity
    {
        public string Name { get; set; } = string.Empty;
    }
}
