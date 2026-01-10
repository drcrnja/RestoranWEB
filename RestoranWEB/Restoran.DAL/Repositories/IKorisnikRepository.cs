using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restoran.DAL.Entities;

namespace Restoran.DAL.Repositories
{
    public interface IKorisnikRepository
    {
        Task<Korisnik?> FirstOrDefaultAsync(Expression<Func<Korisnik, bool>> predicate);
        Task AddAsync(Korisnik k);
        Task<List<Korisnik>> GetAllAsync();
    }
}