using jwt.Models;
using jwt.reposirory;
using jwt.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = model;
            user = db.FirstAcces();
            if (user.Username == "ADM" && model.Password == "ADMIN")
            {
                return new
                {
                    message = "Primeiro Acesso  Seu usuario já existe ||| Por favor troque sua senha por seguranca",
                    user
                }; 
            }
            else
            {


                if (model.Username == "ADM" && model.Password == "ADMIN")
                {
                    /// metodo para trocar Senha
                }
                user.Username = model.Username.ToUpper().Trim();
                user.Password = model.Password.Trim();

                //Recupera o usuário
                user = db.GetUser(user.Username, user.Password);
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

        [HttpPost]
        [Route("AlterarSenha")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> AlterSenha([FromBody]User model)
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
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public async Task<string> Anonymous()
        {

            return "Essa é a Api  Faca o login em https://localhost:44381/vi/account/login ";
        }




    }
}