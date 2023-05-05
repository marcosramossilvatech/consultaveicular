using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Xml;

namespace ConsultaSerpro.Models
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }
        public virtual DbSet<DadosConsulta> DadosConsulta { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<CnhVinculoUsuario> CnhVinculoUsuario { get; set; }

        public virtual DbSet<RetornoMulta> RetornoMulta { get; set; }

        public virtual DbSet<RetornoVeiculo> RetornoVeiculo { get; set; }

        public virtual DbSet<RetornoCNH> RetornoCNH { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DadosConsulta>().HasKey(c => c.Id);

            modelBuilder.Entity<Usuario>().HasKey(c => c.Id);

            modelBuilder.Entity<CnhVinculoUsuario>().HasKey(c => c.Id);

            modelBuilder.Entity<RetornoMulta>().HasKey(c => c.Id);

            modelBuilder.Entity<RetornoVeiculo>().HasKey(c => c.Id);

            modelBuilder.Entity<RetornoCNH>().HasKey(c => c.Id);

        }
    }
}
