using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("Usuário")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
