using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class UsuarioViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória", AllowEmptyStrings = false)]
        [DisplayName("Senha")]
        public string Senha { get; set; }
    }
}
