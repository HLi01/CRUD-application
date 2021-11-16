using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IDriverLogic dl;
        ITeamLogic tl;
        IChampionshipLogic cl;

        public StatController(IDriverLogic dl, ITeamLogic tl, IChampionshipLogic cl)
        {
            this.dl = dl;
            this.tl = tl;
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

        //GET: stat/sumchampbyengines
        public IEnumerable<KeyValuePair<string, int>> SumChampByEngines()
        {
            return tl.SumChampByEngines();
        }

        //GET: stat/debutedandwon/
        [HttpGet("{debutYear}")]
        public bool DebutedAndIsChampion(string debutYear)
        {
            return cl.DebutedAndIsChampion(debutYear);
        }

        //GET: stat/wins/
        [HttpGet("{id}")]
        public int Wins(int id)
        {
            return cl.Wins(id);
        }

        //GET: stat/racenumbers/
        [HttpGet("{number}")]
        public IEnumerable<Championship> RaceNumbers(int number)
        {
            return cl.RaceNumbers(number);
        }
    }
}
