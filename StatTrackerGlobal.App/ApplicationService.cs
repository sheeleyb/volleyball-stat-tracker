using StatTrackerGlobal.App.Interfaces;
using StatTrackerGlobal.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using StatTrackerGlobal.App.DataModels;
using StatTrackerGlobal.Shared;
using Microsoft.AspNetCore.Components;
using static StatTrackerGlobal.App.ViewModels.TeamOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.GameOverviewViewModel;
using static StatTrackerGlobal.App.ViewModels.SetOverviewViewModel;
using System.Security.Cryptography.X509Certificates;
using StatTrackerGlobal.Domain;
using StatTrackerGlobal.Domain.Stats;

namespace StatTrackerGlobal.App
{
    public class ApplicationService : IApplicationService
    {
        private IPersistence Persistence { get; }
        private DomainWrapper DomainWrapper { get; set; }
        private StatTrackerGlobalDataWrapper data { get; set; }

        public ApplicationService(IPersistence persistence)
        {
            Persistence = persistence;
        }
        public MockViewState EditUpdateCurrentGameAction(string TeamAgainst, DateTime Date)
        {
            Predicate<Game> IsCurrentGame = g => ((g.TeamTwo == TeamAgainst) && (g.Date == Date));
            DomainWrapper.CurrentGame = DomainWrapper.Games.Find(IsCurrentGame);
            DomainWrapper.CurrentGame = new Game()
            {
                Date = Date,
                TeamTwo = TeamAgainst,
                TeamOne = DomainWrapper.Team.Name,
                Sets = DomainWrapper.CurrentGame.Sets
            };
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditUpdateCurrentSetAction(string TeamOne, string TeamTwo, DateTime Date, int Order)
        {
            Predicate<Set> IsCurrentSet = s => ((s.TeamOne == TeamOne)  && (s.TeamTwo == TeamTwo) && (s.Order == Order));
            DomainWrapper.CurrentSet = DomainWrapper.Sets.Find(IsCurrentSet);
            DomainWrapper.CurrentSet = new Set()
            {
                TeamOne = TeamOne,
                TeamTwo = TeamTwo,
                Order = Order
            };
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditUpdateCurrentTeamAction(TeamOverviewViewModel TeamVM)
        {
            return new MockViewState() with { TeamViewModel = TeamVM};
        }
        public MockViewState EditUpdatePlayerStatAction(SetOverviewPlayer playerToUpdate, SetOverviewViewModel currentSet)
        {
            Predicate<VolleyballPlayer> playerExists = p => (p.FirstName + p.LastName == playerToUpdate.FirstName + playerToUpdate.LastName);
            VolleyballPlayer? domainPlayerToUpdate = DomainWrapper.Players.Find(playerExists);
            Predicate<DomainStatWrapper> statWrapperExists = s => (s.StatSet.Date == currentSet.Date) || (s.StatSet.Order == 1);
            DomainStatWrapper? domainStatWrapperToUpdate = domainPlayerToUpdate.PlayerStats.Find(statWrapperExists);
            ImmutableList<DomainStatWrapper> newWrappers = domainPlayerToUpdate.PlayerStats.Remove(domainStatWrapperToUpdate);
            AttackingStats newAttacks = new()
            {
                Kills = playerToUpdate.AttackStats.Kills,
                Attempts = playerToUpdate.AttackStats.Attempts,
                Errors = playerToUpdate.AttackStats.Errors
            };
            BlockingStats newBlocks = new()
            {
                KillBlocks = playerToUpdate.BlockStats.KillBlocks,
                Touches = playerToUpdate.BlockStats.Touches,
                BlockErrors = playerToUpdate.BlockStats.BlockErrors
            };
            PassingStats newPasses = new()
            {
                Digs = playerToUpdate.PassStats.Digs,
                BallTouches = playerToUpdate.PassStats.BallTouches,
                BallMisses = playerToUpdate.PassStats.BallMisses
            };
            ServeRecieveStats newServeRecieveStats = new()
            {
                ThreePointPasses = playerToUpdate.ServeRecieveStats.ThreePointPasses,
                TwoPointPasses = playerToUpdate.ServeRecieveStats.TwoPointPasses,
                OnePointPasses = playerToUpdate.ServeRecieveStats.OnePointPasses,
                ZeroPointPasses = playerToUpdate.ServeRecieveStats.ZeroPointPasses
            };
            ServingStats newServeStats = new()
            {
                Aces = playerToUpdate.ServeStats.Aces,
                ServesMade = playerToUpdate.ServeStats.ServesMade,
                ServesMissed = playerToUpdate.ServeStats.ServesMissed
            };
            newWrappers = newWrappers.Add(new DomainStatWrapper() 
            { 
                StatSet = domainStatWrapperToUpdate.StatSet,
                AttackingStats = newAttacks,
                BlockingStats = newBlocks,
                PassingStats = newPasses,
                ServeRecieveStats = newServeRecieveStats,
                ServingStats = newServeStats
            });
            VolleyballPlayer newPlayer = domainPlayerToUpdate with { PlayerStats = newWrappers };
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }

        public MockViewState EditAddGameAction(string TeamAgainst, DateTime Date)
        {
            DomainWrapper.Games = DomainWrapper.Games.Add(new Game()
            {
                TeamOne = DomainWrapper.Team.Name,
                TeamTwo = TeamAgainst,
                Date = Date,
                Sets = []
            });
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditDeleteGameAction(string TeamAgainst, DateTime Date)
        {
            Predicate<Game> Exists = g => ((g.Date == Date) && (g.TeamTwo == TeamAgainst));
            Game? gameToRemove = DomainWrapper.Games.Find(Exists);
            DomainWrapper.Games = DomainWrapper.Games.Remove(gameToRemove);
            SaveCurrentDomainToPersistence();
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditAddGameSetsAction(string TeamAgainst, DateTime Date)
        {
            Predicate<Game> Exists = g => ((g.Date == Date) && (g.TeamTwo == TeamAgainst));
            Game? domainGame = DomainWrapper.Games.Find(Exists);
            Set newSet = new()
            {
                LocalPlayers = DomainWrapper.Players,
                Order = domainGame.Sets.Count(),
                TeamOne = DomainWrapper.Team.Name,
                TeamTwo = TeamAgainst
            };
            domainGame.Sets = domainGame.Sets.Add(newSet);
            DomainWrapper.Sets = DomainWrapper.Sets.Add(newSet);
            SaveCurrentDomainToPersistence();
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditAddPlayerAction(string FirstName, string LastName, int JerseyNumber, string Height, string Position)
        {
            DomainWrapper.Players = DomainWrapper.Players.Add(new VolleyballPlayer()
            {
                FirstName = FirstName,
                LastName = LastName,
                JerseyNumber = JerseyNumber,
                Height = Height,
                Position = Position
            });
            SaveCurrentDomainToPersistence();
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditDeletePlayerAction(string FirstName, string LastName)
        {
            Predicate<VolleyballPlayer> Exists = p => (p.FirstName == FirstName) && (p.LastName == LastName);
            VolleyballPlayer? playerToRemove = DomainWrapper.Players.Find(Exists);
            DomainWrapper.Players = DomainWrapper.Players.Remove(playerToRemove);
            SaveCurrentDomainToPersistence();
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditSaveGamesAction(TeamOverviewViewModel team, ImmutableList<TeamOverviewGame> games)
        {
            ImmutableList<Game> newGames = [];
            foreach (var viewGame in games)
            {
                Predicate<Game> Exists = g => (g.Date == viewGame.Date) && (g.TeamTwo == viewGame.Against);
                Game? tempGame = DomainWrapper.Games.Find(Exists);
                if (tempGame == null)
                {
                    newGames = newGames.Add(new Game()
                    {
                        Date = viewGame.Date,
                        TeamTwo = viewGame.Against
                    });
                } else
                {
                    newGames = newGames.Add(tempGame);
                }
            }
            DomainWrapper.Games = newGames;
            SaveCurrentDomainToPersistence();
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }
        public MockViewState EditInitializeApplicationAction()
        {
            data = LoadCurrentData();
            DomainWrapper = ModelHandler.DataModelsToDomainModels(data);
            return ModelHandler.DomainModelsToViewModels(DomainWrapper);
        }

        private void SaveCurrentDomainToPersistence() => Persistence.Save(ModelHandler.DomainModelsToDataModels(DomainWrapper));
        private StatTrackerGlobalDataWrapper LoadCurrentData() => Persistence.Load();
    }
}
