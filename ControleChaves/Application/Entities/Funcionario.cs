using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public class Funcionario : RemocaoLogica
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Funcao { get; set; }
        public Status Status { get; set; }
        public ICollection<Controle> Retiradas { get; set; }
        public ICollection<Controle> Devolucoes { get; set; }

        public bool IsActive()
        {
            return Status == Status.ATIVO;
        }

        public void MarkAsRemoved()
        {
            Status = Status.INATIVO;
        }
    }
}
