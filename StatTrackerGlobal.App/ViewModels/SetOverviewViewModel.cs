using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.App.ViewModels
{
    public class SetOverviewViewModel
    {
        public int TeamOnePoints { get; set; }
        public int TeamTwoPoints { get; set; }
        public record SetOverviewPlayer(string FirstName,
                                        string LastName, 
                                        int JerseyNumber,
                                        string Height, 
                                        string Position, 
                                        SetOverviewAttacks AttackStats, 
                                        SetOverviewBlocks BlockStats,
                                        SetOverviewPasses PassStats,
                                        SetOverviewServeRecieve ServeRecieveStats,
                                        SetOverviewServe ServeStats);
        public ImmutableList<SetOverviewPlayer> Players { get; init; } = [];

        public record SetOverviewAttacks(int Kills,
                                         int Attempts,
                                         int Errors,
                                         double KillPercentage,
                                         double ErrorPercentage);
        public record SetOverviewBlocks(int KillBlocks,
                                        int Touches,
                                        int BlockErrors,
                                        double TouchPercent);

        public record SetOverviewPasses(int Digs,
                                        int BallTouches,
                                        int BallMisses,
                                        double TouchPercent);

        public record SetOverviewServeRecieve(int ThreePointPasses,
                                              int TwoPointPasses,
                                              int OnePointPasses,
                                              int ZeroPointPasses);

        public record SetOverviewServe(int Aces,
                                       int ServesMade,
                                       int ServesMissed,
                                       int TotalServes,
                                       double ServePercentage);
    }
}
