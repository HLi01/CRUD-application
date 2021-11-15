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
            if (championship.Date.Year!=2021)
            {
                throw new ArgumentException("The year is not 2021!");
            }
            championshipRepo.Create(championship);
        }

        public void Delete(string id)
        {
            championshipRepo.Delete(id);
        }

        public Championship Read(string id)
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

        //how many times did a driver win?
        public int Wins(string name)
        {
            return championshipRepo.ReadAll().Where(x => x.WinnerName == name).Count();
        }

        //Is there a driver who debuted in provided year, and won a race? 
        public bool DebutedAndWon(string debutYear)
        {
            var names = driverRepo.ReadAll().Where(x => x.DebutYear == debutYear).Select(x => x.Name);
            foreach (var item in championshipRepo.ReadAll())
            {
                string name = item.WinnerName;
                if (names.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }
        
        //When was the provided race?
        public DateTime RaceDate(string id)
        {
            return championshipRepo.ReadAll().First(x=>x.RaceID==id).Date;
        }
    }
}
