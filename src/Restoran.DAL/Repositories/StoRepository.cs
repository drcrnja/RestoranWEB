using Microsoft.EntityFrameworkCore;
using Restoran.DAL.Entities;

namespace Restoran.DAL.Repositories
{
    //implementacija za ssto
    public class StoRepository : IStoRepository
    {
        
        private readonly RestoranContext _ctx;
        //konekcija ka bazi
        public StoRepository(RestoranContext ctx) => _ctx = ctx;
        //prikazuje sve iz tabele sto
        public Task<List<Sto>> GetAllAsync() => _ctx.Stolovi.ToListAsync();
        //trazi sto po idu
        public Task<Sto?> GetByIdAsync(int id) => _ctx.Stolovi.FindAsync(id).AsTask();
        //dali uopste postoji sto sa tim idom
        public Task<bool> ExistsAsync(int id) => _ctx.Stolovi.AnyAsync(s => s.IDStola == id);
    }
}
