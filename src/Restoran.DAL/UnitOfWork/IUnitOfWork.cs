using System;
using Restoran.DAL.Repositories;

public interface IUnitOfWork : IDisposable
{
    IStoRepository Stolovi { get; }
    IGostRepository Gosti { get; }
    IRezervacijaRepository Rezervacije { get; }
    IKorisnikRepository Korisnici { get; }
    Task<int> SaveChangesAsync();
    INarudzbinaRepository Narudzbine { get; }
}