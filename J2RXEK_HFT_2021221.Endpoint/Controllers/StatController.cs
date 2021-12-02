using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IDriverLogic dl;
        IChampionshipLogic cl;

        public StatController(IDriverLogic dl, IChampionshipLogic cl)
        {
            this.dl = dl;
            this.cl = cl;
        }

        // GET: stat/evennumbers
        [HttpGet]
        public IEnumerable<Driver> EvenNumbers()
        {
            return dl.EvenNumbers();
        }

        // GET stat/numberofchampions
        [HttpGet]
        public int NumberOfChampions()
        {
            return dl.NumberOfChampions();
        }

        //GET: stat/wins/
        [HttpGet("{id}")]
        public int Wins(int id)
        {
            return cl.Wins(id);
        }

        //GET: stat/racenumber/
        [HttpGet("{number}")]
        public string RaceNumber(int number)
        {
            return cl.RaceNumber(number);
        }

        //GET: stat/champsbyteam
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> ChampsByTeam()
        {
            return cl.ChampsByTeam();
        }

        //GET: stat/firstdriversofteams
        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> FirstDriversOfTeams()
        {
            return cl.FirstDriversOfTeams();
        }

        //GET: stat/avgagebyteams
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AvgAgeByTeam()
        {
            return cl.AvgAgeByTeam();
        }

        //GET: stat/winnerteamingivenyear/
        [HttpGet("{year}")]
        public string WinnerTeamInGivenYear(int year)
        {
            return cl.WinnerTeamInGivenYear(year);
        }
    }
}
