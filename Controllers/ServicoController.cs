using jwt.Models;
using jwt.reposirory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IServico db;
        public ServicoController(IServico _db)
        {
            db = _db;
        }
        // GET: api/Servico
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<IEnumerable<Servico>>> Get()
        {
            if (db.GetAllServico() == null)
            {
                return NotFound(new { message = "Dados não Encontrados na base de dados tente mais tarde !!!" });
            }
            else
            {
                return db.GetAllServico().ToList();

            }



        }

        // GET: api/Servico/5
        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<Servico>> Get(int id)
        {
           var model=new Servico() ;
            if (db.GetServicoById(id) == null)
            {

                return NotFound(new { message = "Dado não encontrad0 !!! entre em contato com administrador  ou tente mais tarde " });

            }
            else
            {
               model = db.remove(id);
            }

            return model ;
        }

        // POST: api/Servico
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Servico>>Post([FromBody] Servico model)
        {
            var servico = db.ValidateServico(model.Categoria.IdCategoria);
            if(servico == null)
            {
                return NotFound(new { message ="Esse Servico ja existe !!!" });

            }
            else
            {
              return servico =  db.insert(model);
            }
            
        }

        // PUT: api/Servico/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Servico>> Put(int id, [FromBody] Servico model)
        {

            var servico = db.GetServicoById(id);
            if(servico == null)
            {
                
                return NotFound(new { message = " Dado não encontrad0 !!!entre em contato com administrador ou tente mais tarde " });

            }
            else
            {
               servico= db.edit(model);  
            }

            return servico;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Servico>> Delete(int id)
        {
            var servico = db.GetServicoById(id);
            if (servico == null)
            {

                return NotFound(new { message = " Dado não encontrad0 !!!entre em contato com administrador ou tente mais tarde " });

            }
            else
            {
                servico = db.remove(id);
            }

            return servico;

        }
    }
}
