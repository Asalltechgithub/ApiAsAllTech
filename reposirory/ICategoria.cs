using jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.reposirory
{
   public interface ICategoria
    {
        Categoria insert(Categoria model);
        Categoria update(Categoria model);
        Categoria remove(int Id);
        IEnumerable<Categoria> GetAllCategorias();
        Categoria GetCategoriaById(int Id);

        Categoria ValidCategoria(string NomeCategoria);
    }
}
