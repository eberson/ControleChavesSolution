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

        public DateTime Data { get; set; }
        public int FuncionarioID { get; set; }
        public string ChaveID { get; set; }
    }
}
