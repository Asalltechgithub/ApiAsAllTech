using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Models;
using jwt.reposirory;
using jwt.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/account/")]
    public class HomeController : ControllerBase
    {
        private readonly IUsuario db;
        public HomeController(IUsuario _db)
        {
            db = _db;
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            //Recupera o usuário
            var user = db.GetUser(model.Username.ToUpper().Trim(), model.Password);

            //Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            //  Gera o Token
            var token = TokenService.GeneraterToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Cadastrar([FromBody]User model)
        {
            if (db.ValidUserName(model.Username.ToUpper().Trim() )!= null)
            {
             
                return NotFound(new { message = "Ja existe um usuario com esse nome" });
            }
            else
            {
                
            Encryption encryption = new Encryption();
            model.Password = encryption.createEncryptPassword(model.Password);
            model.Username = model.Username.ToUpper();
            model =  db.Insert(model);
            model.Password = string.Empty;
            return  new
            {
               message ="Usuario Inserido com sucesso !!! ",
               user = model
            };
        }
       
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public async Task<string> Anonymous()
        {
            
            return "Essa é a Api  Faca o login em https://localhost:44381/vi/account/login ";
        }

        


    }
}