using J2RXEK_HFT_2021221.Models;
using J2RXEK_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace J2RXEK_HFT_2021221.Logic
{
    public class TeamLogic : ITeamLogic
    {
        ITeamRepository teamRepo;
        IDriverRepository driverRepo;
        IChampionshipRepository championshipRepo;

        public TeamLogic(ITeamRepository teamRepo, IDriverRepository driverRepo, IChampionshipRepository championshipRepo)
        {
            this.teamRepo = teamRepo;
            this.driverRepo = driverRepo;
            this.championshipRepo = championshipRepo;
        }
        public void Create(Team team)
        {
            if (team.ChampionshipsWon < 0)
            {
                throw new ArgumentException("The number of championships won can't be a negative number!");
            }
            teamRepo.Create(team);
        }

        public void Delete(int id)
        {
            if (!driverRepo.ReadAll().Any(x=>x.TeamId==id) && !championshipRepo.ReadAll().Any(x=>x.WCC==id))
            {
                ;
                teamRepo.Delete(id);
            }
        }

        public Team Read(int id)
        {
            return teamRepo.Read(id);
        }

        public IEnumerable<Team> ReadAll()
        {
            return teamRepo.ReadAll();
        }

        public void Update(Team team)
        {
            teamRepo.Update(team);
        }
    }
}
