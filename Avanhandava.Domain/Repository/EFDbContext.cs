using Avanhandava.Domain.Models.Admin;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Avanhandava.Domain.Repository
{
    public class EFDbContext: DbContext
    {

        public EFDbContext()
            : base("AvanhandavaConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<CliGrupoPermissao>().HasKey(x => new { x.IdGrupo, x.IdPermissao });
        }

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<FPgto> Fpgto { get; set; }
        public DbSet<GrupoCusto> GrupoCusto { get; set; }
        public DbSet<ItemCusto> ItemCusto { get; set; }
        public DbSet<Pgto> Pgto { get; set; }
        public DbSet<SistemaParametro> SistemaParametro { get; set; }
        public DbSet<TipoCredito> TipoCredito { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
