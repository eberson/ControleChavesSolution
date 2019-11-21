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
        public int Numero { get; set; }
    }
}
