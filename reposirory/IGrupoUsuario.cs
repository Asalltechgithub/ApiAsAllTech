using jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
    public interface IGrupoUsuario
    {
        GrupoUsuario insert(GrupoUsuario model);
        GrupoUsuario update(GrupoUsuario model);
        GrupoUsuario remove(int Id);
        IEnumerable<GrupoUsuario> GetAllGrupoUsuario();
        GrupoUsuario GetGrupoUsuarioById(int Id);
    }
}
