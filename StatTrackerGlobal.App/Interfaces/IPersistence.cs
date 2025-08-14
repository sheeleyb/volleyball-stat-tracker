using StatTrackerGlobal.App.DataModels;
using System.Collections.Immutable;

namespace StatTrackerGlobal.App.Interfaces
{
    public interface IPersistence
    {
        public void Save(StatTrackerGlobalDataWrapper value);
        public StatTrackerGlobalDataWrapper Load();
    }
}
