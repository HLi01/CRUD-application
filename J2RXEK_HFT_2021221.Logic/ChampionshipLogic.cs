using J2RXEK_HFT_2021221.Models;
using J2RXEK_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Logic
{
    public class ChampionshipLogic : IChampionshipLogic
    {
        IChampionshipRepository championshipRepo;
        ITeamRepository teamRepo;
        IDriverRepository driverRepo;
        public ChampionshipLogic(IChampionshipRepository championshipRepo, ITeamRepository teamRepo, IDriverRepository driverRepo)
        {
            this.championshipRepo = championshipRepo;
            this.teamRepo = teamRepo;
            this.driverRepo = driverRepo;
        }
        public void Create(Championship championship)
        {
            if (championship.Year<=1950 || championship.Year>2020)
            {
                throw new ArgumentException("The year must be between 1950 and 2020!");
            }
            championshipRepo.Create(championship);
        }

        public void Delete(int id)
        {
            championshipRepo.Delete(id);
        }

        public Championship Read(int id)
        {
            return championshipRepo.Read(id);
        }

        public IEnumerable<Championship> ReadAll()
        {
            return championshipRepo.ReadAll();
        }

        public void Update(Championship championship)
        {
            championshipRepo.Update(championship);
        }

        //how many times did a team win the WCC?
        public int Wins(int id)
        {
            return championshipRepo.ReadAll().Where(x => x.WCC == id).Count();
        }

        //Is there a driver who debuted in provided year, and that driver is a champion? 
        public bool DebutedAndIsChampion(string debutYear)
        {
            var drivers = driverRepo.ReadAll().Where(x => x.DebutYear == debutYear);
            foreach (var driver in drivers)
            {
                if (driver.IsChampion == true)
                {
                    return true;
                }
            }
            return false;
        }
        
        //Given racenumbers
        public IEnumerable<Championship> RaceNumbers(int number)
        {
            return championshipRepo.ReadAll().Where(x => x.NumberOfRaces == number);
        }
    }
}
