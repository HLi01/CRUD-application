﻿using J2RXEK_HFT_2021221.Endpoint.Services;
using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChampionshipController : ControllerBase
    {
        IChampionshipLogic cl;
        IHubContext<SignalRHub> hub;
        public ChampionshipController(IChampionshipLogic cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("ChampionshipCreated", value);
        }

        // PUT /championship
        [HttpPut]
        public void Put([FromBody] Championship value)
        {
            cl.Update(value);
            this.hub.Clients.All.SendAsync("ChampionshipUpdated", value);
        }

        // DELETE /championship/
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var championshipToDelete = this.cl.Read(id);
            cl.Delete(id);
            this.hub.Clients.All.SendAsync("ChampionshipDeleted", championshipToDelete);
        }
    }
}
