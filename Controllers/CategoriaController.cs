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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria db;
        public  CategoriaController(ICategoria _db)
        {
            db = _db;
        }

        // GET: api/Categoria
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<Categoria>> Get()
        {
            return db.GetAllCategorias();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}", Name = "Categoria")]
        [Authorize(Roles = "1,2")]
        public async Task<Categoria> Get(int id)
        {
            return db.GetCategoriaById(id);
        }

        // POST: api/Categoria
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Categoria>> Post([FromBody] Categoria model)
        {
            var categoria = db.ValidCategoria(model.Nome_Categoria);
            if (categoria == null)
            {

                categoria =  db.insert(model);
                return categoria;
            }
            else
            {
                return NotFound(new
                {
                    message = " Categoria Já existe"
                });
            }





            return model;
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Categoria>> Put(int id, [FromBody] Categoria model)
        {
            var Cat = db.GetCategoriaById(id);
            if (Cat == null)
            {
                return NotFound(new { message = "Ja existe um usuario com esse nome" });
            }
            else
            {

                Cat = db.update(model);
                return Cat;
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var Cat = db.GetCategoriaById(id);
            if (Cat == null)
            {
                return NotFound(new { message = "Ja existe um usuario com esse nome" });
            }
            else
            {
                db.remove(Cat.IdCategoria);
            }

            return Cat;

        }
    }
}
