using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public class Localizacao
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Chave> Chaves { get; set; }
    }
}
