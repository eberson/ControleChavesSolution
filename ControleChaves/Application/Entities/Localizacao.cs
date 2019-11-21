using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public class Localizacao : RemocaoLogica
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public Status Status { get; set; }
        public virtual List<Chave> Chaves { get; set; }
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
