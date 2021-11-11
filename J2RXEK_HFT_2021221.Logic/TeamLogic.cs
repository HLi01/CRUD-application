using J2RXEK_HFT_2021221.Models;
using J2RXEK_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Logic
{
    public class TeamLogic : ITeamLogics
    {
        ITeamRepository teamRepo;

        public TeamLogic(ITeamRepository teamRepo)
        {
            this.teamRepo = teamRepo;
        }
        public void Create(Team team)
        {
            if (team.ChampionshipsWon<0)
            {
                throw new ArgumentException("The number of championship won can't be a negative number!");
            }
            teamRepo.Create(team);
        }

        public void Delete(string name)
        {
            teamRepo.Delete(name);
        }

        public Team Read(string name)
        {
            return teamRepo.Read(name);
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
