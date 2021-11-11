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
        public ChampionshipLogic(IChampionshipRepository championshipRepo, ITeamRepository teamRepo)
        {
            this.championshipRepo = championshipRepo;
            this.teamRepo = teamRepo;
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

        //non-crud - Average position
        public IEnumerable<KeyValuePair<string, int>> AvgPos()
        {
            return championshipRepo.ReadAll().SelectMany(x => x.Teams).GroupBy(x => x.PowerUnit).Select(x => new KeyValuePair<string, int>(x.Key, x.Sum(x => x.ChampionshipsWon)));
        }

        //how many times did Max Verstappen win?
        public int MaxWins()
        {
            return championshipRepo.ReadAll().Where(x => x.result[0].Name == "Max Verstappen").Count();
        }

        public int Points(Driver driver, List<Driver> list)
        {
            int pointer = list.IndexOf(driver);
            return Championship.points.ElementAt(pointer);
        }
        //How many poins did Vettel score in Monaco? 
        public int VettelPoints()
        {
            Driver SV = new Driver() { Name = "Sebastian Vettel"};
            int sum = 0;
            foreach (var item in championshipRepo.ReadAll().Select(x=>x.result))
            {
                sum += Points(SV, item);
            }
            return sum;
        }
    }
}
