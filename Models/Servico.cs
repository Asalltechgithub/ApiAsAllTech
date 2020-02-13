using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Models
{
    public class Servico
    {
        public int Id_Servico { get; set; }
        public Categoria Categoria { get; set; }
        public string Descricao { get; set; }
    }
}
