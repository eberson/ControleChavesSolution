using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class ControleViewModel
    {
        [Key]
        public int Codigo { get; set; }
        
        [Required(ErrorMessage = "A data da retirada é obrigatória")]
        [DataType(DataType.DateTime, ErrorMessage = "A data de retirada informada não é válida")]
        [DisplayName("Data de Retirada")]
        public DateTime Retirada { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "A data de devolução informada não é válida")]
        [DisplayName("Data de Devolução")]
        public DateTime Devolucao { get; set; }

        [Required(ErrorMessage = "O usuário que retirou a chave é obrigatório")]
        [DisplayName("Usuário da Retirada")]
        public FuncionarioViewModel FuncionarioRetirada { get; set; }

        [DisplayName("Usuário da Devolução")]
        public FuncionarioViewModel FuncionarioDevolucao { get; set; }

        [Required(ErrorMessage = "A chave movimentada é obrigatória")]
        [DisplayName("Chave")]
        public ChaveViewModel Chave { get; set; }
    }
}
