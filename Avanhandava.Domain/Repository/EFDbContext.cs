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

        public DbSet<SistemaParametro> SistemaParametro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
