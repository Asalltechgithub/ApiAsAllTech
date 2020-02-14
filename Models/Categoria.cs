using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "Numeros de Caracteres Exedido", ErrorMessageResourceName = "Error", MinimumLength = 25)]
        public string Nome_Categoria { get; set; }

    }
}
