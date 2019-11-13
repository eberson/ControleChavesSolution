using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public class Chave
    {
        public int Numero { get; set; }
        public virtual Localizacao Localizacao { get; set; }
        public ICollection<Controle> Movimentacoes { get; set; }
    }
}
