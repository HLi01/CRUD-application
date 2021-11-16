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
    public class DriverController : ControllerBase
    {
        IDriverLogic dl;
        public DriverController(IDriverLogic dl)
        {
            this.dl = dl;
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
        }

        // PUT /driver
        [HttpPut]
        public void Put([FromBody] Driver value)
        {
            dl.Update(value);
        }

        // DELETE /driver/
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dl.Delete(id);
        }
    }
}
