using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Models;

namespace jwt.reposirory
{
    public class GrupoUsuarioRepository : IGrupoUsuario
    {
        public GrupoUsuario edit(GrupoUsuario model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GrupoUsuario> GetAllGrupoUsuario()
        {
            List<GrupoUsuario> array = new List<GrupoUsuario>();

            array.Add(
                new GrupoUsuario()
                {
                    IdGrupoUsuario =1,
                    NomeGrupoUsuario = "manager"
                });
            array.Add(
               new GrupoUsuario()
               {
                   IdGrupoUsuario = 2,
                   NomeGrupoUsuario = "employee"
               });
            array.Add(
              new GrupoUsuario()
              {
                  IdGrupoUsuario = 3,
                  NomeGrupoUsuario = "client"
              });
            return array;
        }

        public IEnumerable<GrupoUsuario> GetAllServico()
        {
            throw new NotImplementedException();
        }

        public GrupoUsuario GetGrupoUsuarioById(int Id)
        {
            throw new NotImplementedException();
        }

        public GrupoUsuario GetServicoById(int Id)
        {
            throw new NotImplementedException();
        }

        public GrupoUsuario insert(GrupoUsuario model)
        {
            throw new NotImplementedException();
        }

        public GrupoUsuario remove(int Id)
        {
            throw new NotImplementedException();
        }

        public GrupoUsuario update(GrupoUsuario model)
        {
            throw new NotImplementedException();
        }
    }
}
