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
    public class TeamController : ControllerBase
    {
        ITeamLogic tl;
        public TeamController(ITeamLogic tl)
        {
            this.tl = tl;
        }

        // GET: /team
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return tl.ReadAll();
        }

        // GET /team/
        [HttpGet("{id}")]
        public Team Get(string name)
        {
            return tl.Read(name);
        }

        // POST /team
        [HttpPost]
        public void Post([FromBody] Team value)
        {
            tl.Create(value);
        }

        // PUT /team
        [HttpPut]
        public void Put([FromBody] Team value)
        {
            tl.Update(value);
        }

        // DELETE /team/
        [HttpDelete("{id}")]
        public void Delete(string name)
        {
            tl.Delete(name);
        }
    }
}
