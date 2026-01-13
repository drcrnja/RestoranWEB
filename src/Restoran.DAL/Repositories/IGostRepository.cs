using System.Linq.Expressions;   // Omogućuje da pišemo ime sebicnog gosta
using Restoran.DAL.Entities;      // koristi klasu sebicnog gosta

namespace Restoran.DAL.Repositories
{
    // šta sve možemo da tražimo od gosta
    public interface IGostRepository
    {
       
        // Daje  prvog sebicnog gosta koji zadovoljava bilo koji uslov
        Task<Gost?> FirstOrDefaultAsync(Expression<Func<Gost, bool>> predicate);

        // Dodaje novog sebicnog gosta u bazu 
        Task AddAsync(Gost gost);
    }
}