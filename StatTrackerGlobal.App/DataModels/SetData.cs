using System.Collections.Immutable;

namespace StatTrackerGlobal.App.DataModels
{
    public record SetData : Entity
    {
        public int TeamOneScore { get; init; }
        public int TeamTwoScore { get; init; }
        public int Order { get; init; }
        public ImmutableList<StatEventDatum> StatEvents { get; init; } = [];
    }
}
