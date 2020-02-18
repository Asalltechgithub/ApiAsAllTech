using jwt.Models;
using jwt.reposirory;
using jwt.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Tuple<Token,User> >> Authenticate([FromBody]User model)
        {
            var user = model;
            if (db.FirstAcces() == false)
            {
                model = new User()
                {
                    Username = " ADM ",
                    Password = " ADMIN ",
                    Grupo = new GrupoUsuario
                    {
                        IdGrupoUsuario = 1
                    }

                };
                var ADM = db.Insert(model);

                return Ok(new
                {
                    message = "Foi gerado o usuario Master no seu primeiro acesso Recomendo que altere a senha ",
                    ADM
                });
               
            }



            user.Username = model.Username.ToUpper().Trim();
            user.Password = model.Password.Trim();

            //Recupera o usuário
            user = db.GetUser(user.Username, user.Password);
            //Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            Token token = new Token();
           
            //  Gera o Token
           token.AcessToken = TokenService.GeneraterToken(user);
           

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new Tuple<Token,User>(token,user);
            
           

        }
        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Cadastrar([FromBody]User model)
        {

            if (db.ValidUserName(model.Username.ToUpper().Trim()) != null)
            {

                return NotFound(new { message = "Ja existe um usuario com esse nome" });
            }
            else
            {

                Encryption encryption = new Encryption();
                model.Password = encryption.createEncryptPassword(model.Password);
                model.Username = model.Username.ToUpper();
                model = db.Insert(model);
                model.Password = string.Empty;

                return model;
            }

        }

        [HttpPut]
        [Route("AlterarSenha")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> AlterSenha(string newPass,[FromBody]User model )
        {



            var user = db.GetUser(model.Username, model.Password);
            if (user == null)
            {
                return NotFound(new { message = "Usuario não existe ou senha Incorreta !!!" });
            }
            else
            {
                model.Id = user.Id;
                Encryption encryption = new Encryption();
                model.Password = encryption.createEncryptPassword(model.Password);
                model = db.EditPassword(model);
                model.Password = string.Empty;
            }

            return model;


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