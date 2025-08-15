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
