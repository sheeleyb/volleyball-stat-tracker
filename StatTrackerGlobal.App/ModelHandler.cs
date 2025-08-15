using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Domain;
using StatTrackerGlobal.Domain.Stats;
using StatTrackerGlobal.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
                    Predicate<Set> setChecker = s => (s.TeamOne == playerStat.StatSet.TeamOne) && (s.TeamTwo == playerStat.StatSet.TeamTwo);
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
                Players = setPlayers
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
                            if ((playerStat.StatSet.TeamOne == set.TeamOne) && (playerStat.StatSet.TeamTwo == set.TeamTwo))
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
                        Order = setToAdd.Order
                    });
                    Set domainSet = new Set()
                    {
                        TeamOne = data.TeamData.Name,
                        TeamTwo = gameData.TeamTwo,
                        TeamOneScore = setToAdd.TeamOneScore,
                        TeamTwoScore = setToAdd.TeamTwoScore,
                        Order = setToAdd.Order
                    };
                    foreach (var statEvent in setToAdd.StatEvents)
                    {
                        Predicate<PlayerData> playerStatMatch = p => (statEvent.PlayerId == p.Id);
                        PlayerData? playerDataToAdd = data.PlayerData.Find(playerStatMatch);
                        Predicate<VolleyballPlayer> playerDomainMatch = p => ((playerDataToAdd.FirstName + playerDataToAdd.LastName) == (p.FirstName + p.LastName));
                        VolleyballPlayer? playerToEdit = Players.Find(playerDomainMatch);
                        Predicate<Set> matchSet = s => ((s.TeamOne == domainSet.TeamOne) && (s.TeamTwo == domainSet.TeamTwo));
                        foreach (var statWrapper in playerToEdit.PlayerStats)
                        {
                            if (matchSet(statWrapper.StatSet))
                            {
                                AttackingStats newAttacks = new();
                                BlockingStats newBlocks = new();
                                PassingStats newPasses = new();
                                ServeRecieveStats newServeRecieves = new();
                                ServingStats newServes = new();
                                DomainStatWrapper newWrapper = new();
                                switch(statEvent.StatEvent)
                                {
                                    case StatEvent.Kill:
                                        newAttacks = statWrapper.AttackingStats with { Kills = statWrapper.AttackingStats.Kills + 1};
                                        newWrapper = statWrapper with { AttackingStats =  newAttacks};
                                        break;
                                    case StatEvent.Attempt:
                                        newAttacks = statWrapper.AttackingStats with { Attempts = statWrapper.AttackingStats.Attempts + 1 };
                                        newWrapper = statWrapper with { AttackingStats = newAttacks };
                                        break;
                                    case StatEvent.Error:
                                        newAttacks = statWrapper.AttackingStats with { Kills = statWrapper.AttackingStats.Errors + 1 };
                                        newWrapper = statWrapper with { AttackingStats = newAttacks };
                                        break;
                                    case StatEvent.KillBlock:
                                        newBlocks = statWrapper.BlockingStats with { KillBlocks = statWrapper.BlockingStats.KillBlocks + 1 };
                                        newWrapper = statWrapper with { BlockingStats = newBlocks };
                                        break;
                                    case StatEvent.Touch:
                                        newBlocks = statWrapper.BlockingStats with { Touches = statWrapper.BlockingStats.Touches + 1 };
                                        newWrapper = statWrapper with { BlockingStats = newBlocks };
                                        break;
                                    case StatEvent.BlockError:
                                        newBlocks = statWrapper.BlockingStats with { BlockErrors = statWrapper.BlockingStats.BlockErrors + 1 };
                                        newWrapper = statWrapper with { BlockingStats = newBlocks };
                                        break;
                                    case StatEvent.Dig:
                                        newPasses = statWrapper.PassingStats with { Digs = statWrapper.PassingStats.Digs + 1 };
                                        newWrapper = statWrapper with { PassingStats = newPasses };
                                        break;
                                    case StatEvent.BallTouch:
                                        newPasses = statWrapper.PassingStats with { BallTouches = statWrapper.PassingStats.BallTouches + 1 };
                                        newWrapper = statWrapper with { PassingStats = newPasses };
                                        break;
                                    case StatEvent.BallMiss:
                                        newPasses = statWrapper.PassingStats with { BallMisses = statWrapper.PassingStats.BallMisses + 1 };
                                        newWrapper = statWrapper with { PassingStats = newPasses };
                                        break;
                                    case StatEvent.ThreePointPass:
                                        newServeRecieves = statWrapper.ServeRecieveStats with { ThreePointPasses = statWrapper.ServeRecieveStats.ThreePointPasses + 1 };
                                        newWrapper = statWrapper with { ServeRecieveStats = newServeRecieves };
                                        break;
                                    case StatEvent.TwoPointPass:
                                        newServeRecieves = statWrapper.ServeRecieveStats with { TwoPointPasses = statWrapper.ServeRecieveStats.TwoPointPasses + 1 };
                                        newWrapper = statWrapper with { ServeRecieveStats = newServeRecieves };
                                        break;
                                    case StatEvent.OnePointPass:
                                        newServeRecieves = statWrapper.ServeRecieveStats with { OnePointPasses = statWrapper.ServeRecieveStats.OnePointPasses + 1 };
                                        newWrapper = statWrapper with { ServeRecieveStats = newServeRecieves };
                                        break;
                                    case StatEvent.ZeroPointPass:
                                        newServeRecieves = statWrapper.ServeRecieveStats with { ZeroPointPasses = statWrapper.ServeRecieveStats.ZeroPointPasses + 1 };
                                        newWrapper = statWrapper with { ServeRecieveStats = newServeRecieves };
                                        break;
                                    case StatEvent.Ace:
                                        newServes = statWrapper.ServingStats with { Aces = statWrapper.ServingStats.Aces + 1 };
                                        newWrapper = statWrapper with { ServingStats = newServes };
                                        break;
                                    case StatEvent.ServeMade:
                                        newServes = statWrapper.ServingStats with { ServesMade = statWrapper.ServingStats.ServesMade + 1 };
                                        newWrapper = statWrapper with { ServingStats = newServes };
                                        break;
                                    case StatEvent.ServeMissed:
                                        newServes = statWrapper.ServingStats with { ServesMissed = statWrapper.ServingStats.ServesMissed + 1 };
                                        newWrapper = statWrapper with { ServingStats = newServes };
                                        break;
                                }
                                playerToEdit.PlayerStats = playerToEdit.PlayerStats.Remove(statWrapper);
                                playerToEdit.PlayerStats = playerToEdit.PlayerStats.Add(newWrapper);

                            }
                        }
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
