﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        [DisplayName("E-mail")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória", AllowEmptyStrings = false)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
