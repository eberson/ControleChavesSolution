using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class LocalizacaoViewModel
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "A descrição do local é obrigatória", AllowEmptyStrings = false)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public List<ChaveViewModel> Chaves { get; set; }
    }
}
