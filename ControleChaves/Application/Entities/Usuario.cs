using System;
using System.Collections.Generic;

namespace ControleChaves.Application.Entities
{
    public class Usuario : RemocaoLogica
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
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
