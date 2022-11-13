using Microsoft.EntityFrameworkCore;
using Model;

namespace DevTour.Database
{
    public class DestinoDbContext : DbContext
    {
        public DestinoDbContext(DbContextOptions<DestinoDbContext> options) : base(options) {

        }

        public DbSet<Destino> Destinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var destino = modelBuilder.Entity<Destino>();
            destino.ToTable("destino");
            destino.HasKey(x => x.Id);
            destino.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            destino.Property(x => x.DestinoViagem).HasColumnName("destino").IsRequired();
            destino.Property(x => x.Partida).HasColumnName("partida").IsRequired();
            destino.Property(x => x.Transporte).HasColumnName("transporte").IsRequired();


        }
    }
}