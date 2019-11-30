using Bogus;
using ControleChaves.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Database
{
    public static class ControleChavesSeeder
    {
        public static void Seed(this ControleChavesContext context)
        {
            SeedUsers(context);
            SeedFuncionarios(context);
            SeedLocalizacoes(context);

            context.SaveChanges();
        }

        private static void SeedUsers(ControleChavesContext context)
        {
            var usuarios = new Faker<Usuario>("pt_BR")
                .RuleFor(u => u.Email, f => f.Person.Email.ToLower())
                .RuleFor(u => u.Nome, f => f.Person.FullName)
                .RuleFor(u => u.Senha, f => f.Internet.Password())
                .Generate(10);

            foreach (var usuario in usuarios)
            {
                context.Usuarios.Add(usuario);
            }
        }

        private static void SeedFuncionarios(ControleChavesContext context)
        {
            var funcionarios = new Faker<Funcionario>("pt_BR")
                .RuleFor(e => e.Celular, f => f.Person.Phone)
                .RuleFor(e => e.DataNascimento, f => f.Date.Past(30))
                .RuleFor(e => e.Email, f => f.Person.Email.ToLower())
                .RuleFor(e => e.Nome, f => f.Person.FullName)
                .Generate(80);

            foreach(var f in funcionarios)
            {
                f.Funcao = "PROFESSOR";
                context.Funcionarios.Add(f);
            }

        }

        private static void SeedLocalizacoes(ControleChavesContext context)
        {
            for (int i = 0; i < 30; i++)
            {
                var text = i < 15 ? "Sala {0}" : "Laboratório {0}";
                var numero = i + 1;

                Localizacao l = new Localizacao
                {
                    Descricao = string.Format(text, numero),
                    Status = Status.ATIVO
                };

                context.Localizacoes.Add(l);

                Chave chave = new Chave
                {
                    Localizacao = l,
                    Numero = numero.ToString(),
                    Status = Status.ATIVO
                };

                context.Chaves.Add(chave);
            }
        }
    }
}
