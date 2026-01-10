using Restoran.DAL.Entities;

namespace Restoran.DAL.Repositories
{
    public interface IStoRepository
    {
        //prikazuje sve stolove
        Task<List<Sto>> GetAllAsync();
        //sto po idu
       
        Task<bool> ExistsAsync(int id);
    }
}
