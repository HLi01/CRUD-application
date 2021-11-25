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
        //Returns the driver's name whose number is equal to the given number
        public string RaceNumber(int number)
        {
            return teamRepo.ReadAll().SelectMany(x=>x.Drivers).Where(x=>x.Number==number).FirstOrDefault().Name;
        }
        //Returns the number of champions by team
        public IEnumerable<KeyValuePair<string, int>> ChampsByTeam()
        {
            return from x in teamRepo.ReadAll().AsEnumerable()
                   group x by x.TeamName into g
                   select new KeyValuePair<string, int>(g.Key, g.SelectMany(x => x.Drivers).Where(x => x.IsChampion == true).Count());
        }
        //Returns the first driver's name of the teams(the driver who debuted first)
        public IEnumerable<KeyValuePair<string, string>> FirstDriversOfTeams()
        {
            return from x in teamRepo.ReadAll().AsEnumerable()
                   group x by x.TeamName into g
                   select new KeyValuePair<string, string>(g.Key, g.SelectMany(x => x.Drivers).OrderBy(x => x.DebutYear).FirstOrDefault().Name);
        }
        //Average ages by teams
        public IEnumerable<KeyValuePair<string, double>> AvgAgeByTeam()
        {
            return from x in teamRepo.ReadAll().AsEnumerable()
                   group x by x.TeamName into g
                   select new KeyValuePair<string, double>(g.Key, g.SelectMany(x => x.Drivers).Average(x=>x.Age));
        }
        //Which team won in the given year?
        public string WinnerTeamInGivenYear(int year)
        {
            int winnerid = championshipRepo.ReadAll().Where(x => x.Year == year).Select(x => x.WCC).FirstOrDefault();
            //return championshipRepo.ReadAll().Select(x=>x.Team).Where(x=>x.Id== championshipRepo.ReadAll().Where(x => x.Year == year).Select(x => x.WCC).FirstOrDefault()).Select(x=>x.TeamName).FirstOrDefault();
            return championshipRepo.ReadAll().Where(x => x.WCC == winnerid).Select(x => x.Team).FirstOrDefault().TeamName;
        }
    }
}
