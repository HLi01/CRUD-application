using J2RXEK_HFT_2021221.Endpoint.Services;
using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamLogic tl;
        IHubContext<SignalRHub> hub;
        public TeamController(ITeamLogic tl, IHubContext<SignalRHub> hub)
        {
            this.tl = tl;
            this.hub = hub;
        }

        // GET: /team
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return tl.ReadAll();
        }

        // GET /team/
        [HttpGet("{id}")]
        public Team Get(int id)
        {
            return tl.Read(id);
        }

        // POST /team
        [HttpPost]
        public void Post([FromBody] Team value)
        {
            tl.Create(value);
            this.hub.Clients.All.SendAsync("TeamCreated", value);
        }

        // PUT /team
        [HttpPut]
        public void Put([FromBody] Team value)
        {
            tl.Update(value);
            this.hub.Clients.All.SendAsync("TeamUpdated", value);
        }

        // DELETE /team/
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var teamToDelete = this.tl.Read(id);
            tl.Delete(id);
            this.hub.Clients.All.SendAsync("TeamDeleted", teamToDelete);
        }
    }
}
