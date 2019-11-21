using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public class Chave : RemocaoLogica
    {
        public string Numero { get; set; }
        public Status Status { get; set; }
        public virtual Localizacao Localizacao { get; set; }
        public ICollection<Controle> Movimentacoes { get; set; }
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
