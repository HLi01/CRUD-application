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

        //how many times did a driver win?
        public int Wins(string name)
        {
            return championshipRepo.ReadAll().Where(x => x.result[0].Name == name).Count();
        }

        public int Points(Driver driver, List<Driver> list)
        {
            int[] points = new int[] { 25, 18, 15, 12, 10, 8, 6, 4, 2, 1 };
            int pointer = list.IndexOf(driver);
            return points.ElementAt(pointer);
        }
        //How many poins did Vettel score? 
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
        //How many podiums? 
        public bool HasPodium(string name)
        {
            foreach (var item in championshipRepo.ReadAll())
            {
                if (item.result.Take(3).Contains(new Driver() {Name=name }))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
