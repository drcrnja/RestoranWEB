using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restoran.DAL.Entities;

namespace Restoran.DAL.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly RestoranContext _ctx;
        public KorisnikRepository(RestoranContext ctx) => _ctx = ctx;

        public Task<Korisnik?> FirstOrDefaultAsync(Expression<Func<Korisnik, bool>> predicate) =>
            _ctx.Korisnici.FirstOrDefaultAsync(predicate);

        public Task AddAsync(Korisnik k) => _ctx.Korisnici.AddAsync(k).AsTask();

        public Task<List<Korisnik>> GetAllAsync() => _ctx.Korisnici.ToListAsync();
    }
}