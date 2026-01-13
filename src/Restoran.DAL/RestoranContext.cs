using Microsoft.EntityFrameworkCore;
using Restoran.DAL.Entities;

namespace Restoran.DAL
{
    public class RestoranContext : DbContext
    {
        public RestoranContext(DbContextOptions<RestoranContext> opts) : base(opts) { }

        public DbSet<Gost> Gosti => Set<Gost>();
        public DbSet<Sto> Stolovi => Set<Sto>();
        public DbSet<Rezervacija> Rezervacije => Set<Rezervacija>();
        public DbSet<Korisnik> Korisnici => Set<Korisnik>();
        public DbSet<Narudzbina> Narudzbine => Set<Narudzbina>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

         
            b.Entity<Rezervacija>()
             .HasOne(r => r.Narudzbina)
             .WithOne(n => n.Rezervacija)
             .HasForeignKey<Rezervacija>(r => r.NarudzbinaId)
             .OnDelete(DeleteBehavior.SetNull);  
            b.Entity<Korisnik>().HasIndex(k => k.Ime).IsUnique();

            
            b.Entity<Korisnik>().HasData(
                new Korisnik { Id = 1, Ime = "menadzer", Lozinka = "1234", Uloga = Uloga.Menadzer, Aktivan = true },
                new Korisnik { Id = 2, Ime = "kuvar", Lozinka = "1234", Uloga = Uloga.Kuvar, Aktivan = true },
                new Korisnik { Id = 3, Ime = "konobar", Lozinka = "1234", Uloga = Uloga.Konobar, Aktivan = true }
            );
            b.Entity<Gost>().HasKey(g => g.IDGosta);
            b.Entity<Sto>().HasKey(s => s.IDStola);
            b.Entity<Rezervacija>().HasKey(r => r.IDRezervacije);

            b.Entity<Gost>().Property(g => g.IDGosta).UseIdentityColumn();
            b.Entity<Sto>().Property(s => s.IDStola).UseIdentityColumn();
            b.Entity<Rezervacija>().Property(r => r.IDRezervacije).UseIdentityColumn();

            b.Entity<Rezervacija>()
                .HasOne(r => r.Gost)
                .WithMany(g => g.Rezervacije)
                .HasForeignKey(r => r.IDGosta)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<Rezervacija>()
                .HasOne(r => r.Sto)
                .WithMany(s => s.Rezervacije)
                .HasForeignKey(r => r.IDStola)
                .OnDelete(DeleteBehavior.Cascade);

            
            b.Entity<Gost>().ToTable("Gost");
            b.Entity<Sto>().ToTable("Sto");
            b.Entity<Rezervacija>().ToTable("Rezervacija");  
        }
    }
}