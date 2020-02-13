using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Models
{
    public class Portifolio
    {
        public int Id_Portifolio { get; set; }
        public Categoria categoria { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Link { get; set; }
    }
}
