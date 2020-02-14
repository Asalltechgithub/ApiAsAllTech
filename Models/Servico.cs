using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Models
{
    public class Servico
    {
        [Key]
        public int Id_Servico { get; set; }
        [Required]
        public Categoria Categoria { get; set; }
        [Required]
        [StringLength(280,ErrorMessage ="Numeros de Caracteres Exedido",ErrorMessageResourceName ="Error",MinimumLength =25)]
        public string Descricao { get; set; }
    }
}
