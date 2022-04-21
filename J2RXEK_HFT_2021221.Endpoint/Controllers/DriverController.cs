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
    public class DriverController : ControllerBase
    {
        IDriverLogic dl;
        IHubContext<SignalRHub> hub;
        public DriverController(IDriverLogic dl, IHubContext<SignalRHub> hub)
        {
            this.dl = dl;
            this.hub = hub;
        }

        // GET: /driver
        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            return dl.ReadAll();
        }

        // GET /driver/
        [HttpGet("{id}")]
        public Driver Get(int id)
        {
            return dl.Read(id);
        }

        // POST /driver
        [HttpPost]
        public void Post([FromBody] Driver value)
        {
            dl.Create(value);
            this.hub.Clients.All.SendAsync("DriverCreated", value);
        }

        // PUT /driver
        [HttpPut]
        public void Put([FromBody] Driver value)
        {
            dl.Update(value);
            this.hub.Clients.All.SendAsync("DriverUpdated", value);
        }

        // DELETE /driver/
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var driverToDelete = this.dl.Read(id);
            dl.Delete(id);
            this.hub.Clients.All.SendAsync("DriverDeleted", driverToDelete);
        }
    }
}
