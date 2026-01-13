using System.Collections.Generic;
using System.Threading.Tasks;
using Restoran.DAL.Entities;

namespace Restoran.DAL.Repositories
{
    public interface INarudzbinaRepository
    {
        Task<List<Narudzbina>> GetAllAsync();
        Task<Narudzbina?> GetByIdAsync(int id);
        Task AddAsync(Narudzbina n);
        Task<List<Narudzbina>> GetByUserAsync(string userName);
        Task DeleteAsync(Narudzbina entity);

        
        Task<Narudzbina?> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<Narudzbina, bool>> predicate);
    }
}