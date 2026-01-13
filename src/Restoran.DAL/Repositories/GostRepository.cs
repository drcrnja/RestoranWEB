using Microsoft.EntityFrameworkCore; 
using Restoran.DAL.Entities;
using System.Linq.Expressions;

namespace Restoran.DAL.Repositories
{
    //Implementacija za sebicnog gosta
    public class GostRepository : IGostRepository
    {
        // konekcija ka bazi
        private readonly RestoranContext _context;

        // konekcija za unit of work
        public GostRepository(RestoranContext context)
        {
            _context = context;
        }

        // Prikazije sve iz tabele sebicnog gosta i prikazuje kao tabelu
      
        public Task<List<Gost>> GetAllAsync() =>
            _context.Gosti.ToListAsync();

        // Trazi sebicnog gosta po idu
        public Task<Gost?> GetByIdAsync(int id) =>
            _context.Gosti.FindAsync(id).AsTask();

        // Trazi prvog sebicnog gosta koji zadovoljava bilo koji uslov
        public Task<Gost?> FirstOrDefaultAsync(Expression<Func<Gost, bool>> predicate) =>
            _context.Gosti.FirstOrDefaultAsync(predicate);

        // Dodaje novog sebicnog gosta u bazu
        // unit of work ce da sacuva promene
        public Task AddAsync(Gost gost) =>
            _context.Gosti.AddAsync(gost).AsTask();
    }
}