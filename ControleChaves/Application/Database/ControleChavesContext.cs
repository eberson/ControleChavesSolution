using ControleChaves.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Database
{
    public class ControleChavesContext: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Chave> Chaves { get; set; }
        public DbSet<Controle> Controles { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=bdControledeChaves;user=root;password=123@#qwe");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(u =>
            {
                u.HasKey(e => e.ID);
                u.Property(e => e.ID).ValueGeneratedOnAdd();
                u.Property(e => e.Nome).IsRequired();
                u.Property(e => e.Email).IsRequired();
                u.Property(e => e.Senha).IsRequired();
                u.Property(e => e.Status).HasDefaultValue(Status.ATIVO);
            });

            modelBuilder.Entity<Funcionario>(f =>
            {
                f.HasKey(e => e.ID);
                f.Property(e => e.ID).ValueGeneratedOnAdd();
                f.Property(e => e.Nome).IsRequired();
                f.Property(e => e.Celular).IsRequired();
                f.Property(e => e.Email).IsRequired();
                f.Property(e => e.Status).HasDefaultValue(Status.ATIVO);
            });

            modelBuilder.Entity<Localizacao>(l =>
            {
                l.HasKey(e => e.ID);
                l.Property(e => e.ID).ValueGeneratedOnAdd();
                l.Property(e => e.Descricao).IsRequired();
                l.Property(e => e.Status).HasDefaultValue(Status.ATIVO);
            });

            modelBuilder.Entity<Chave>(c =>
            {
                c.HasKey(e => e.Numero);
                c.HasOne(e => e.Localizacao).WithMany(l => l.Chaves).IsRequired();
                c.Property(e => e.Numero).HasMaxLength(20);
                c.Property(e => e.Status).HasDefaultValue(Status.ATIVO);
            });

            modelBuilder.Entity<Controle>(c =>
           {
               c.Property(e => e.Codigo).ValueGeneratedOnAdd();
               c.HasKey(e => e.Codigo);
               c.Property(e => e.Retirada).HasDefaultValue(DateTime.Now);
               c.HasOne(e => e.UsuarioRetirada).WithMany(u => u.Retiradas).IsRequired();
               c.HasOne(e => e.UsuarioDevolucao).WithMany(u => u.Devolucoes);
               c.HasOne(e => e.FuncionarioRetirada).WithMany(f => f.Retiradas).IsRequired();
               c.HasOne(e => e.FuncionarioDevolucao).WithMany(f => f.Devolucoes);
               c.HasOne(e => e.Chave).WithMany(c => c.Movimentacoes).IsRequired();
           });
        }
    }
}
