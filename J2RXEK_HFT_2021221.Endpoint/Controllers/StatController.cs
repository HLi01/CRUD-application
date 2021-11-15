using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public bool DebutedAndWon(string debutYear)
        {
            return cl.DebutedAndWon(debutYear);
        }

        //GET: stat/wins/
        [HttpGet("{name}")]
        public int Wins(string name)
        {
            return cl.Wins(name);
        }

        //GET: stat/racedate/
        [HttpGet("{id}")]
        public DateTime RaceDate(string id)
        {
            return cl.RaceDate(id);
        }
    }
}
