using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Domain;
using StatTrackerGlobal.Domain.Stats;
using StatTrackerGlobal.Shared;
using System.Collections.Immutable;
using static StatTrackerGlobal.App.ViewModels.GameOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.SetOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.TeamOverviewViewModel;

namespace StatTrackerGlobal.App
{
    public class ModelHandler
    {
        public static VolleyballPlayer ViewToDomain(TeamOverviewPlayer teamPlayer)
        {
            return new VolleyballPlayer()
            {
                FirstName = teamPlayer.FirstName,
                LastName = teamPlayer.LastName,
                JerseyNumber = teamPlayer.JerseyNumber,
                Height = teamPlayer.Height,
                Position = teamPlayer.Position
            };
        }

        public static MockViewState DomainModelsToViewModels(DomainWrapper domain)
        {
            ImmutableList<TeamOverviewPlayer> teamPlayers = [];
            ImmutableList<TeamOverviewGame> teamGames = [];
            ImmutableList<GameOverviewPlayer> gamePlayers = [];
            ImmutableList<GameOverviewViewModel> gameViewModels = [];
            ImmutableList<GameOverviewSet> gameSets = [];
            ImmutableList<SetOverviewPlayer> setPlayers = [];
            SetOverviewViewModel tempSet = new();
            foreach (var player in domain.Players)
            {
                teamPlayers = teamPlayers.Add(new TeamOverviewPlayer(player.FirstName,
                                                                     player.LastName,
                                                                     player.JerseyNumber,
                                                                     player.Height,
                                                                     player.Position));
                gamePlayers = gamePlayers.Add(new GameOverviewPlayer(player.FirstName,
                                                                     player.LastName,
                                                                     player.JerseyNumber,
                                                                     player.Height,
                                                                     player.Position,
                                                                     false));
                SetOverviewPlayer tempPlayer = new SetOverviewPlayer(player.FirstName,
                                                                  player.LastName,
                                                                  player.JerseyNumber,
                                                                  player.Height,
                                                                  player.Position,
                                                                  new SetOverviewAttacks(0, 0, 0, 0, 0),
                                                                  new SetOverviewBlocks(0, 0, 0, 0),
                                                                  new SetOverviewPasses(0, 0, 0, 0),
                                                                  new SetOverviewServeRecieve(0, 0, 0, 0),
                                                                  new SetOverviewServe(0, 0, 0, 0, 0));
                foreach (var playerStat in player.PlayerStats)
                {
                    Predicate<Set> setChecker = s => (s.Date == playerStat.StatSet.Date) && (s.Order == playerStat.StatSet.Order);
                    if (setChecker(domain.CurrentSet))
                    {
                        tempPlayer = tempPlayer with
                        {
                            AttackStats = new SetOverviewAttacks(playerStat.AttackingStats.Kills,
                                                                 playerStat.AttackingStats.Attempts,
                                                                 playerStat.AttackingStats.Errors,
                                                                 playerStat.AttackingStats.KillPercentage,
                                                                 playerStat.AttackingStats.ErrorPercentage),
                            BlockStats = new SetOverviewBlocks(playerStat.BlockingStats.KillBlocks,
                                                                playerStat.BlockingStats.Touches,
                                                                playerStat.BlockingStats.BlockErrors,
                                                                playerStat.BlockingStats.TouchPercent),
                            PassStats = new SetOverviewPasses(playerStat.PassingStats.Digs,
                                                              playerStat.PassingStats.BallTouches,
                                                              playerStat.PassingStats.BallMisses,
                                                              playerStat.PassingStats.TouchPercent),
                            ServeRecieveStats = new SetOverviewServeRecieve(playerStat.ServeRecieveStats.ThreePointPasses,
                                                                            playerStat.ServeRecieveStats.TwoPointPasses,
                                                                            playerStat.ServeRecieveStats.OnePointPasses,
                                                                            playerStat.ServeRecieveStats.ZeroPointPasses),
                            ServeStats = new SetOverviewServe(playerStat.ServingStats.Aces,
                                                              playerStat.ServingStats.ServesMade,
                                                              playerStat.ServingStats.ServesMissed,
                                                              playerStat.ServingStats.TotalServes,
                                                              playerStat.ServingStats.ServePercentages)
                        };
                    }
                }
                setPlayers = setPlayers.Add(tempPlayer);
            }
            foreach (var game in domain.Games)
            {
                int tempOrder = 1;
                foreach (var set in game.Sets)
                {
                    if ((game.Date == domain.CurrentGame.Date) && (game.TeamTwo == domain.CurrentGame.TeamTwo))
                    {
                        gameSets = gameSets.Add(new GameOverviewSet(gamePlayers, tempOrder));
                        tempOrder++;
                    }
                }
                teamGames = teamGames.Add(new TeamOverviewGame(game.TeamTwo,
                                                               game.Winner,
                                                               game.Score,
                                                               game.Date));
            }
            SetOverviewViewModel setViewModel = new SetOverviewViewModel()
            {
                TeamOnePoints = domain.CurrentSet.TeamOneScore,
                TeamTwoPoints = domain.CurrentSet.TeamTwoScore,
                Players = setPlayers,
                Date = domain.CurrentGame.Date
            };
            TeamOverviewViewModel teamViewModel = new TeamOverviewViewModel()
            {
                TeamName = domain.Team.Name,
                Games = teamGames,
                Players = teamPlayers

            };
            GameOverviewViewModel gameViewModel = new GameOverviewViewModel()
            {
                Date = domain.CurrentGame.Date,
                TeamAgainst = domain.CurrentGame.TeamTwo,
                Players = gamePlayers,
                Sets = gameSets
            };
            return new MockViewState() with { TeamViewModel = teamViewModel, GameViewModel = gameViewModel, SetViewModel = setViewModel };
        }
        public static StatTrackerGlobalDataWrapper DomainModelsToDataModels(DomainWrapper domain)
        {
            ImmutableList<GameData> gameData;
            ImmutableList<SetData> setData;
            ImmutableList<PlayerData> playerData;
            TeamData teamData;
            gameData = [];
            setData = [];
            playerData = [];
            teamData = new TeamData()
            {
                Name = domain.Team.Name,
                Id = Guid.NewGuid()
            };
            foreach (var player in domain.Players)
            {
                playerData = playerData.Add(new PlayerData()
                {
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    JerseyNumber = player.JerseyNumber,
                    Height = player.Height,
                    Position = player.Position,
                    TeamId = teamData.Id
                });
            }
            foreach (var game in domain.Games)
            {
                ImmutableList<Guid> SetGuids = [];
                foreach (var set in game.Sets)
                {
                    Guid setId = Guid.NewGuid();
                    ImmutableList<StatEventDatum> statData = [];
                    SetGuids = SetGuids.Add(setId);
                    foreach (var player in domain.Players)
                    {
                        foreach (var playerStat in player.PlayerStats)
                        {
                            if ((playerStat.StatSet.Date == set.Date) && (playerStat.StatSet.Order == set.Order))
                            {
                                Predicate<PlayerData> matchDataPlayer = p => ((p.FirstName + p.LastName) == (player.FirstName + player.LastName));
                                PlayerData? matchedPlayer = playerData.Find(matchDataPlayer);
                                for (int i = 0; i < playerStat.AttackingStats.Kills; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.Kill));
                                }
                                for (int i = 0; i < playerStat.AttackingStats.Attempts; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.Attempt));
                                }
                                for (int i = 0; i < playerStat.AttackingStats.Errors; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.Error));
                                }
                                for (int i = 0; i < playerStat.BlockingStats.KillBlocks; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.KillBlock));
                                }
                                for (int i = 0; i < playerStat.BlockingStats.Touches; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.Touch));
                                }
                                for (int i = 0; i < playerStat.BlockingStats.BlockErrors; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.BlockError));
                                }
                                for (int i = 0; i < playerStat.PassingStats.Digs; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.Dig));
                                }
                                for (int i = 0; i < playerStat.PassingStats.BallTouches; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.BallTouch));
                                }
                                for (int i = 0; i < playerStat.PassingStats.BallMisses; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.BallMiss));
                                }
                                for (int i = 0; i < playerStat.ServeRecieveStats.ThreePointPasses; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.ThreePointPass));
                                }
                                for (int i = 0; i < playerStat.ServeRecieveStats.TwoPointPasses; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.TwoPointPass));
                                }
                                for (int i = 0; i < playerStat.ServeRecieveStats.OnePointPasses; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.OnePointPass));
                                }
                                for (int i = 0; i < playerStat.ServeRecieveStats.ZeroPointPasses; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.ZeroPointPass));
                                }
                                for (int i = 0; i < playerStat.ServingStats.Aces; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.Ace));
                                }
                                for (int i = 0; i < playerStat.ServingStats.ServesMade; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.ServeMade));
                                }
                                for (int i = 0; i < playerStat.ServingStats.ServesMissed; i++)
                                {
                                    statData = statData.Add(new StatEventDatum(matchedPlayer.Id, StatEvent.ServeMissed));
                                }
                            }
                        }
                    }
                    setData = setData.Add(new SetData()
                    {
                        TeamOneScore = set.TeamOneScore,
                        TeamTwoScore = set.TeamTwoScore,
                        Order = set.Order,
                        Id = setId,
                        StatEvents = statData
                    });
                }
                gameData = gameData.Add(new GameData()
                {
                    TimeStamp = game.Date,
                    TeamTwo = game.TeamTwo,
                    Sets = SetGuids,
                    Id = Guid.NewGuid()
                });
            }
            return new StatTrackerGlobalDataWrapper()
            {
                GameData = gameData,
                SetData = setData,
                PlayerData = playerData,
                TeamData = teamData
            };
        }
        public static DomainWrapper DataModelsToDomainModels(StatTrackerGlobalDataWrapper data)
        {
            Team team;
            Game CurrentGame;
            ImmutableList<Game> Games = [];
            ImmutableList<Set> Sets = [];
            ImmutableList<VolleyballPlayer> Players = [];
            foreach (var playerData in data.PlayerData)
            {
                Players = Players.Add(new VolleyballPlayer()
                {
                    FirstName = playerData.FirstName,
                    LastName = playerData.LastName,
                    Team = data.TeamData.Name,
                    JerseyNumber = playerData.JerseyNumber,
                    Height = playerData.Height,
                    Position = playerData.Position
                });
            }
            foreach (var gameData in data.GameData)
            {
                ImmutableList<Set> GameSets = [];
                foreach (var setGuid in gameData.Sets)
                {
                    Predicate<SetData> isMatching = setData => setData.Id.Equals(setGuid);
                    SetData? setToAdd = data.SetData.Find(isMatching);
                    GameSets = GameSets.Add(new Set()
                    {
                        TeamOne = data.TeamData.Name,
                        TeamTwo = gameData.TeamTwo,
                        TeamOneScore = setToAdd.TeamOneScore,
                        TeamTwoScore = setToAdd.TeamTwoScore,
                        Order = setToAdd.Order,
                        Date = gameData.TimeStamp
                    });
                    Set domainSet = new Set()
                    {
                        TeamOne = data.TeamData.Name,
                        TeamTwo = gameData.TeamTwo,
                        TeamOneScore = setToAdd.TeamOneScore,
                        TeamTwoScore = setToAdd.TeamTwoScore,
                        Order = setToAdd.Order,
                        Date = gameData.TimeStamp
                    };
                    Sets = Sets.Add(domainSet);
                    foreach (var statEvent in setToAdd.StatEvents)
                    {
                        Predicate<PlayerData> playerStatMatch = p => (statEvent.PlayerId == p.Id);
                        PlayerData? playerDataToAdd = data.PlayerData.Find(playerStatMatch);
                        Predicate<VolleyballPlayer> playerDomainMatch = p => ((playerDataToAdd.FirstName + playerDataToAdd.LastName) == (p.FirstName + p.LastName));
                        VolleyballPlayer? playerToEdit = Players.Find(playerDomainMatch);
                        Predicate<DomainStatWrapper> setMatch = s => (s.StatSet.Date == domainSet.Date) && (s.StatSet.Order == setToAdd.Order);
                        DomainStatWrapper? domainStatWrapper = playerToEdit.PlayerStats.Find(setMatch);
                        if (domainStatWrapper == null)
                        {
                            domainStatWrapper = new DomainStatWrapper()
                            {
                                StatSet = domainSet
                            };
                            playerToEdit.PlayerStats = playerToEdit.PlayerStats.Add(domainStatWrapper);
                        }
                        AttackingStats newAttacks = new();
                        BlockingStats newBlocks = new();
                        PassingStats newPasses = new();
                        ServeRecieveStats newServeRecieves = new();
                        ServingStats newServes = new();
                        DomainStatWrapper newWrapper = new()
                        {
                            StatSet = domainSet
                        };
                        switch (statEvent.StatEvent)
                        {
                            case StatEvent.Kill:
                                newAttacks = domainStatWrapper.AttackingStats with { Kills = domainStatWrapper.AttackingStats.Kills + 1 };
                                newWrapper = domainStatWrapper with { AttackingStats = newAttacks };
                                break;
                            case StatEvent.Attempt:
                                newAttacks = domainStatWrapper.AttackingStats with { Attempts = domainStatWrapper.AttackingStats.Attempts + 1 };
                                newWrapper = domainStatWrapper with { AttackingStats = newAttacks };
                                break;
                            case StatEvent.Error:
                                newAttacks = domainStatWrapper.AttackingStats with { Errors = domainStatWrapper.AttackingStats.Errors + 1 };
                                newWrapper = domainStatWrapper with { AttackingStats = newAttacks };
                                break;
                            case StatEvent.KillBlock:
                                newBlocks = domainStatWrapper.BlockingStats with { KillBlocks = domainStatWrapper.BlockingStats.KillBlocks + 1 };
                                newWrapper = domainStatWrapper with { BlockingStats = newBlocks };
                                break;
                            case StatEvent.Touch:
                                newBlocks = domainStatWrapper.BlockingStats with { Touches = domainStatWrapper.BlockingStats.Touches + 1 };
                                newWrapper = domainStatWrapper with { BlockingStats = newBlocks };
                                break;
                            case StatEvent.BlockError:
                                newBlocks = domainStatWrapper.BlockingStats with { BlockErrors = domainStatWrapper.BlockingStats.BlockErrors + 1 };
                                newWrapper = domainStatWrapper with { BlockingStats = newBlocks };
                                break;
                            case StatEvent.Dig:
                                newPasses = domainStatWrapper.PassingStats with { Digs = domainStatWrapper.PassingStats.Digs + 1 };
                                newWrapper = domainStatWrapper with { PassingStats = newPasses };
                                break;
                            case StatEvent.BallTouch:
                                newPasses = domainStatWrapper.PassingStats with { BallTouches = domainStatWrapper.PassingStats.BallTouches + 1 };
                                newWrapper = domainStatWrapper with { PassingStats = newPasses };
                                break;
                            case StatEvent.BallMiss:
                                newPasses = domainStatWrapper.PassingStats with { BallMisses = domainStatWrapper.PassingStats.BallMisses + 1 };
                                newWrapper = domainStatWrapper with { PassingStats = newPasses };
                                break;
                            case StatEvent.ThreePointPass:
                                newServeRecieves = domainStatWrapper.ServeRecieveStats with { ThreePointPasses = domainStatWrapper.ServeRecieveStats.ThreePointPasses + 1 };
                                newWrapper = domainStatWrapper with { ServeRecieveStats = newServeRecieves };
                                break;
                            case StatEvent.TwoPointPass:
                                newServeRecieves = domainStatWrapper.ServeRecieveStats with { TwoPointPasses = domainStatWrapper.ServeRecieveStats.TwoPointPasses + 1 };
                                newWrapper = domainStatWrapper with { ServeRecieveStats = newServeRecieves };
                                break;
                            case StatEvent.OnePointPass:
                                newServeRecieves = domainStatWrapper.ServeRecieveStats with { OnePointPasses = domainStatWrapper.ServeRecieveStats.OnePointPasses + 1 };
                                newWrapper = domainStatWrapper with { ServeRecieveStats = newServeRecieves };
                                break;
                            case StatEvent.ZeroPointPass:
                                newServeRecieves = domainStatWrapper.ServeRecieveStats with { ZeroPointPasses = domainStatWrapper.ServeRecieveStats.ZeroPointPasses + 1 };
                                newWrapper = domainStatWrapper with { ServeRecieveStats = newServeRecieves };
                                break;
                            case StatEvent.Ace:
                                newServes = domainStatWrapper.ServingStats with { Aces = domainStatWrapper.ServingStats.Aces + 1 };
                                newWrapper = domainStatWrapper with { ServingStats = newServes };
                                break;
                            case StatEvent.ServeMade:
                                newServes = domainStatWrapper.ServingStats with { ServesMade = domainStatWrapper.ServingStats.ServesMade + 1 };
                                newWrapper = domainStatWrapper with { ServingStats = newServes };
                                break;
                            case StatEvent.ServeMissed:
                                newServes = domainStatWrapper.ServingStats with { ServesMissed = domainStatWrapper.ServingStats.ServesMissed + 1 };
                                newWrapper = domainStatWrapper with { ServingStats = newServes };
                                break;
                        }
                        playerToEdit.PlayerStats = playerToEdit.PlayerStats.Remove(domainStatWrapper);
                        playerToEdit.PlayerStats = playerToEdit.PlayerStats.Add(newWrapper);
                    }

                }
                Games = Games.Add(new Game()
                {
                    Date = gameData.TimeStamp,
                    TeamOne = data.TeamData.Name,
                    TeamTwo = gameData.TeamTwo,
                    Sets = GameSets
                });
            }
            team = new Team()
            {
                Name = data.TeamData.Name
            };
            CurrentGame = new Game()
            {
                Sets = [],
                Date = DateTime.Now,
                TeamOne = team.Name,
                TeamTwo = ""
            };
            return new DomainWrapper()
            {
                Team = team,
                CurrentGame = CurrentGame,
                Games = Games,
                Sets = Sets,
                Players = Players
            };


        }
    }
}
