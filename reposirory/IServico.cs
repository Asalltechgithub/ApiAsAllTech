using jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
    public interface IServico
    {
        Servico insert(Servico model);
        Servico edit(Servico model);
        Servico remove(int Id);
        Servico GetServicoById(int Id);
        IEnumerable<Servico> GetAllServico();
        bool ValidateServico(string Descricao);
    }
}
