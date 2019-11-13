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
                u.Property(e => e.Celular).IsRequired();
                u.Property(e => e.Email).IsRequired();
                u.Property(e => e.Senha).IsRequired();
            });

            modelBuilder.Entity<Localizacao>(l =>
            {
                l.HasKey(e => e.ID);
                l.Property(e => e.ID).ValueGeneratedOnAdd();
                l.Property(e => e.Descricao).IsRequired();
            });

            modelBuilder.Entity<Chave>(c =>
            {
                c.HasKey(e => e.Numero);
                c.Property(e => e.Numero).ValueGeneratedOnAdd();
                c.HasOne(e => e.Localizacao).WithMany(l => l.Chaves);
            });

            modelBuilder.Entity<Controle>(c =>
           {
               c.Property(e => e.Codigo).ValueGeneratedOnAdd();
               c.HasKey(e => e.Codigo);
               c.Property(e => e.Retirada).HasDefaultValue(DateTime.Now);
               c.HasOne(e => e.UsuarioRetirada).WithMany(u => u.Retiradas).IsRequired();
               c.HasOne(e => e.UsuarioDevolucao).WithMany(u => u.Devolucoes).IsRequired();
               c.HasOne(e => e.Chave).WithMany(c => c.Movimentacoes).IsRequired();
           });
        }
    }
}
