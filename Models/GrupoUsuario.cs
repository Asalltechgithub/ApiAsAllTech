using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Models
{
    public class GrupoUsuario
    {
        [Key]
        public int IdGrupoUsuario { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Numeros de Caracteres Exedido", ErrorMessageResourceName = "Error", MinimumLength = 3)]
        public string NomeGrupoUsuario { get; set; }
    }
}
