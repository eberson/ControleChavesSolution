using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Models
{
    public class EmprestimoViewModel
    {
       
        [Required(ErrorMessage = "A data da retirada é obrigatória")]
        [DataType(DataType.DateTime, ErrorMessage = "A data de retirada informada não é válida")]
        [DisplayName("Data de Retirada")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O usuário que retirou a chave é obrigatório")]
        [DisplayName("Usuário da Retirada")]
        public FuncionarioViewModel Funcionario { get; set; }

        [Required(ErrorMessage = "A chave movimentada é obrigatória")]
        [DisplayName("Chave")]
        public ChaveViewModel Chave { get; set; }
    }
}
