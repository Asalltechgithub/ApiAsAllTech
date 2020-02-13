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
    public class GrupoUsuarioController : ControllerBase
    {
        private readonly IGrupoUsuario db;
        public GrupoUsuarioController(IGrupoUsuario _db)
        {
            db = _db;
        }
        // GET: api/GrupoUsuario
        [HttpGet]
        public async Task<IEnumerable<GrupoUsuario>> Get()
        {
            return db.GetAllGrupoUsuario();
        }

        // GET: api/GrupoUsuario/5
        [HttpGet("{id}", Name = "GetbyId")]
        public async Task<ActionResult<GrupoUsuario>> Get(int id)
        {

            var model = db.GetGrupoUsuarioById(id);
            if (model == null)
            {
                return NotFound(new { message = "Dados não Encontrados na base de dados tente mais tarde !!!" });
            }
            else
            {
                return model;
            }


        }

        // POST: api/GrupoUsuario
        [HttpPost]
        public async Task<ActionResult<GrupoUsuario>> Post([FromBody] GrupoUsuario model)
        {
            model.NomeGrupoUsuario.ToUpper();
           if(model != null)
            {
                return BadRequest(new { message = "Dados invalidos" });
            }

            else
            {
              return  db.insert(model);
            }
        }

        // PUT: api/GrupoUsuario/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GrupoUsuario>> Put(int id, [FromBody] GrupoUsuario model)
        {

            model.NomeGrupoUsuario.ToUpper();

             var gu =  db.GetGrupoUsuarioById(id);
            if (gu == null)
            {
                return NotFound(new { message = "Dados não encontrados na base !!! "});
            }

            else
            {
                return db.update(model);
            }


        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GrupoUsuario>> Delete(int id)
        {


            var gu = db.GetGrupoUsuarioById(id);
            if (gu == null)
            {
                return NotFound(new { message = "Dados não encontrados na base !!! " });
            }

            else
            {
                return db.remove(gu.IdGrupoUsuario);
            }

        }
    }
}
