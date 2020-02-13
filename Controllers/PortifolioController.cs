using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Models;
using jwt.reposirory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortifolioController : ControllerBase
    {
        private readonly IPortifolio db;
        public PortifolioController(IPortifolio _db)
        {
            db = _db;
        }
        // GET: api/Portifolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portifolio>>> Get()
        {
            return db.GetAllPortifolio().ToList();
        }

        // GET: api/Portifolio/5
        [HttpGet("{id}", Name = "GetPortifolioByID")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Portifolio
        [HttpPost]
        public async Task<ActionResult<Portifolio>> Post([FromBody] Portifolio model)
        {

            throw new NotImplementedException();
        }

        // PUT: api/Portifolio/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
