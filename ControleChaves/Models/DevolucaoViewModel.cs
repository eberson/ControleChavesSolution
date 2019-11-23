using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class DevolucaoViewModel
    {
        [Key]
        [Required(ErrorMessage = "É necessário saber qual movimentação está sendo encerrada")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "A data da devolução é obrigatória")]
        [DataType(DataType.DateTime, ErrorMessage = "A data de devolução informada não é válida")]
        [DisplayName("Data da Devolução")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O usuário que devolveu a chave é obrigatório")]
        [DisplayName("Usuário da Devolução")]
        public FuncionarioViewModel Funcionario { get; set; }
    }
}
