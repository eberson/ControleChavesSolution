﻿using ControleChaves.Application.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class ChaveViewModel
    {
        [Required]
        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        public int LocalizacaoID { get; set; }
    }
}
