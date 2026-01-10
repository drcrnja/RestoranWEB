using Microsoft.EntityFrameworkCore;
using Restoran.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restoran.DAL.Repositories
{
    public class RezervacijaRepository : IRezervacijaRepository
    {
        private readonly RestoranContext _ctx;
        public RezervacijaRepository(RestoranContext ctx) => _ctx = ctx;

        public Task<List<Rezervacija>> GetAllAsync() =>
            _ctx.Rezervacije
                .Include(r => r.Gost)
                .Include(r => r.Sto)
                .Include(r => r.Narudzbina)   
                .ToListAsync();

        public Task<Rezervacija?> GetByIdAsync(int id) =>
            _ctx.Rezervacije
                .Include(r => r.Gost)
                .Include(r => r.Sto)
                .Include(r => r.Narudzbina)
                .FirstOrDefaultAsync(r => r.IDRezervacije == id);

        public Task AddAsync(Rezervacija rez) => _ctx.Rezervacije.AddAsync(rez).AsTask();

        public Task DeleteAsync(int id) => _ctx.Rezervacije.Where(r => r.IDRezervacije == id).ExecuteDeleteAsync();

        public Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<Rezervacija, bool>> predicate) =>
            _ctx.Rezervacije.AnyAsync(predicate);
    }
}