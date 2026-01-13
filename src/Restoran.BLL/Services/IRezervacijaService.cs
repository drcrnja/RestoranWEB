using System.Collections.Generic;
using System.Threading.Tasks;
using Restoran.BLL.DTOs;

namespace Restoran.BLL.Services
{
    // definise sta sve moze da se trazi

    public interface IRezervacijaService
    {
        // Daj mi spisak svih rezervacija 
        Task<List<RezervacijaDto>> GetAllAsync();

        // Daj mi  rezervaciju po ID-ju
        Task<RezervacijaDto?> GetByIdAsync(int id);

        
        // Greska ako je sto zauzet
        Task<int> CreateAsync(RezervacijaDto dto);

        // Izmeni postojeću 
        Task UpdateAsync(RezervacijaDto dto);

        // Obriši rezervaciju 
        Task DeleteAsync(int id);
    }
}