using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChampionshipController : ControllerBase
    {
        IChampionshipLogic cl;
        public ChampionshipController(IChampionshipLogic cl)
        {
            this.cl = cl;
        }

        // GET: /championship
        [HttpGet]
        public IEnumerable<Championship> Get()
        {
            return cl.ReadAll();
        }

        // GET /championship/
        [HttpGet("{id}")]
        public Championship Get(int id)
        {
            return cl.Read(id);
        }

        // POST /championship
        [HttpPost]
        public void Post([FromBody] Championship value)
        {
            cl.Create(value);
        }

        // PUT /championship
        [HttpPut]
        public void Put([FromBody] Championship value)
        {
            cl.Update(value);
        }

        // DELETE /championship/
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cl.Delete(id);
        }
    }
}
