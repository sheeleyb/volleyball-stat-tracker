using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using StatTrackerGlobal.Domain;
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
                setPlayers = setPlayers.Add(new SetOverviewPlayer(player.FirstName,
                                                                  player.LastName,
                                                                  player.JerseyNumber,
                                                                  player.Height,
                                                                  player.Position,
                                                                  new SetOverviewAttacks(0, 0, 0, 0, 0, 0),
                                                                  new SetOverviewBlocks(0, 0, 0, 0),
                                                                  new SetOverviewPasses(0, 0, 0, 0),
                                                                  new SetOverviewServeRecieve(0, 0, 0, 0),
                                                                  new SetOverviewServe(0, 0, 0, 0, 0)));
                foreach (var playerStat in player.PlayerStats)
                {
                    Predicate<Set> setChecker = s => (s.TeamOne == playerStat.StatSet.TeamOne) && (s.TeamTwo == playerStat.StatSet.TeamTwo);

                }
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
            return new MockViewState() with { TeamViewModel = teamViewModel, GameViewModel = gameViewModel };
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
            foreach (var game in domain.Games)
            {
                ImmutableList<Guid> SetGuids = [];
                foreach (var set in game.Sets)
                {
                    Guid setId = Guid.NewGuid();
                    SetGuids = SetGuids.Add(setId);
                    setData = setData.Add(new SetData()
                    {
                        TeamOneScore = set.TeamOneScore,
                        TeamTwoScore = set.TeamTwoScore,
                        Order = set.Order,
                        Id = setId
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
                }
                Games = Games.Add(new Game()
                {
                    Date = gameData.TimeStamp,

                    TeamOne = data.TeamData.Name,
                    TeamTwo = gameData.TeamTwo,
                    Sets = GameSets
                });
            }
            foreach (var setData in data.SetData)
            {
                Sets = Sets.Add(new Set()
                {
                    Order = setData.Order,
                    TeamOneScore = setData.TeamOneScore,
                    TeamTwoScore = setData.TeamTwoScore
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
