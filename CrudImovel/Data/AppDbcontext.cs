using Microsoft.EntityFrameworkCore;

namespace CrudImovel.Data
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }

        public DbSet<Imovel> Imovel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imovel>().Property(P => P.Cep).HasMaxLength(8);
                   
        }
    }
}
