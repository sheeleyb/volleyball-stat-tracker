

namespace StatTrackerGlobal.App.DataModels
{
    public record PlayerData : Entity
    {
        public Guid TeamId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public int JerseyNumber { get; init; }
        public string Height { get; init; } = string.Empty;
        public string Position { get; init; } = string.Empty;
    }
}
