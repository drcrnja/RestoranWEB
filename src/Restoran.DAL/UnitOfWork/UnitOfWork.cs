using Restoran.DAL.Entities;
using Restoran.DAL.Repositories;

namespace Restoran.DAL.UnitOfWork
{
    //spaja sve u jednu celinu
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestoranContext _ctx;

        public IStoRepository Stolovi { get; }
        public IGostRepository Gosti { get; }
        public IRezervacijaRepository Rezervacije { get; }
        public IKorisnikRepository Korisnici { get; }
        public INarudzbinaRepository Narudzbine { get; }
        public UnitOfWork(RestoranContext ctx)
        {
            _ctx = ctx;
            Stolovi = new StoRepository(_ctx);
            Gosti = new GostRepository(_ctx);
            Rezervacije = new RezervacijaRepository(_ctx);
            Korisnici = new KorisnikRepository(_ctx);
            Narudzbine = new NarudzbinaRepository(_ctx);
        }

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
        public void Dispose() => _ctx?.Dispose();
    }
}