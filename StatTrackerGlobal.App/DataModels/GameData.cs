using System.Collections.Immutable;

namespace StatTrackerGlobal.App.DataModels
{
    public record GameData : Entity
    {
        public DateTime TimeStamp { get; init; }
        public String TeamTwo { get; init; } = string.Empty;
        public ImmutableList<Guid> Sets { get; init; } = [];
    }
}
