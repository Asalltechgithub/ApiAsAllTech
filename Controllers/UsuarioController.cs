using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Models;
using jwt.reposirory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario db;
        public UsuarioController(IUsuario _db)
        {
            db = _db;
        }
        // GET: api/Usuario
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult< IEnumerable<User>>> GetAllUsuarios()
        {
            if (db.GetAllUsers()==null)
            {

                return NotFound(new { message = "Dados não Encontrados na base de dados tente mais tarde !!!" });
            }
            else
            {
                return db.GetAllUsers().ToList();
            }

           
        }

        // GET: api/Usuario/5
        [HttpGet]
        [Route("Perfil/{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<User>> GetUsersByRole(int id)
        {
            return db.GetUsersByRole(id);
        }

        // POST: api/Usuario
        [HttpPost]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<User>> Post([FromBody] User model)
        {
            var usuario = db.ValidUserName(model.Username);
            if (usuario==null)
            {
                return db.Insert(model);
            }
            else
            {

                return NotFound(new
                {
                    message = "Ja existe um usuario com esse nome"
                });
            }
        }

        // PUT: api/Usuario/5
        [HttpPost]
        [Route("Edit")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<User>> Put([FromBody] User model)
        {
             
            if (db.GetUserById(model.Id) == null)
            {
                return NotFound();
            }
            else
            {
                
                model = db.Edit(model);
            }

            return model;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Route("remove/{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User model = new User();
            
            model = db.GetUserById(id);
            if (model == null)
            {
                return NotFound();
            }
            else
            {
                
                db.Remove(model);
                
            }

            return model;
        }
    }
}
