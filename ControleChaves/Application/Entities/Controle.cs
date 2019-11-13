using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public class Controle
    {
        public int Codigo { get; set; }
        public DateTime Retirada { get; set; }
        public DateTime Devolucao { get; set; }
        public virtual Usuario UsuarioRetirada { get; set; }

        public virtual Usuario UsuarioDevolucao { get; set; }

        public virtual Chave Chave { get; set; }
    }
}
