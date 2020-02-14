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
        public async Task<ActionResult<Portifolio>> Get(int id)
        {
            var model = db.GetPortifolioById(id);
          if ( model != null)
            {

                return model;
            }
            else
            {
                return NotFound(new {message = "Dados não encontrados" });
            }
            
        }

        // POST: api/Portifolio
        [HttpPost]
        public async Task<ActionResult<Portifolio>> Post([FromBody] Portifolio model)
        {
          var Model =  db.Insert(model);
            if(Model == null)
            {
                return BadRequest(new { Message = "Dados Invalidos Revise os dados e tente denovo" });
            }

            return Model;
        }

        // PUT: api/Portifolio/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Portifolio>> Put(int id, [FromBody] Portifolio model)
        {
          var Model =  db.GetPortifolioById(id);
            if (Model != null)
            {

            return   db.Edit(model);
            }
            else
            {
                return NotFound(new { Message = "Dados Invalidos ou inexistentes Revise os dados e tente denovo" });
            }
           

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Portifolio>>  Delete(int id)
        {
            var Model = db.GetPortifolioById(id);
            if (Model != null)
            {

                return  db.remove(id);
            }
            else
            {
                return NotFound(new { Message = "Dados Invalidos ou inexistentes Revise os dados e tente denovo" });
            }

        }
    }
}
