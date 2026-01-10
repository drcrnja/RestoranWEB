using Restoran.DAL.Entities;

namespace Restoran.DAL.Repositories
{
    //definise sta sve moze da se radi sa rezervacijama
    public interface IRezervacijaRepository
    {
        //sve rezervacije sa gostom i stolovima
        Task<List<Rezervacija>> GetAllAsync();
        //rezervacija po idu sa sebicnim gostom i stolom
        Task<Rezervacija?> GetByIdAsync(int id);
        //dodaje rezervaciju
        Task AddAsync(Rezervacija rez);
        //brise sve rezervacije koje odgovaraju uslovu
        Task DeleteAsync(int id);
        //dali postoji rezervacija koja odgovara uslovu
        Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<Rezervacija, bool>> predicate);
    }
}
