using System;
using System.Collections.Generic;

namespace ControleChaves.Application.Entities
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Funcao { get; set; }
        public ICollection<Controle> Retiradas { get; set; }
        public ICollection<Controle> Devolucoes { get; set; }
    }
}
