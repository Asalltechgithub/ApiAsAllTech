using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Models
{
    public class Portifolio
    {
        public int Id_Portifolio { get; set; }
        [Required]
         public Categoria categoria { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Numeros de Caracteres Exedido", ErrorMessageResourceName = "Error", MinimumLength = 10)]
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
