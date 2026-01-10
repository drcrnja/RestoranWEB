using AutoMapper;
using Restoran.BLL.DTOs;
using Restoran.DAL.Entities;
using Restoran.DAL.UnitOfWork;

namespace Restoran.BLL.Services
{
    //logika za rad sa rezervacijama
    public class RezervacijaService : IRezervacijaService
    {
        //uow za rad sa bazom
        private readonly IUnitOfWork _uow;
        //mapper za mapiranje izmedju dto i entiteta
        private readonly IMapper _mapper;
        
        public RezervacijaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        //prikaz svih rezervacija
        public async Task<List<RezervacijaDto>> GetAllAsync() =>
            _mapper.Map<List<RezervacijaDto>>(await _uow.Rezervacije.GetAllAsync());
        //priakaz rezervacija po idu
        public async Task<RezervacijaDto?> GetByIdAsync(int id) =>
            _mapper.Map<RezervacijaDto>(await _uow.Rezervacije.GetByIdAsync(id));
        //kreira nove rezervacije
        public async Task<int> CreateAsync(RezervacijaDto dto)
        {
            //proverava dali je sto zauzet za dati datum
            bool zauzet = await _uow.Rezervacije
                .AnyAsync(r => r.IDStola == dto.BrojStola && r.Datum == dto.Datum);
            //ako je zauzet
            if (zauzet)
                throw new InvalidOperationException($"Sto {dto.BrojStola} je zauzet.");
            //proverava dali gost postoji
            var gost = await _uow.Gosti
                .FirstOrDefaultAsync(g => g.ImeGosta == dto.ImeGosta &&
                                          g.PrezimeGosta == dto.PrezimeGosta);
            //ako ne postoji kreira novog gosta
            if (gost == null)
            {
                gost = new Gost
                {
                   
                    ImeGosta = dto.ImeGosta,
                    PrezimeGosta = dto.PrezimeGosta
                };
                // dodaje sebicnog gosta gosta
                await _uow.Gosti.AddAsync(gost);
                //cuva promene
                await _uow.SaveChangesAsync();
            }

            // pronadji sto
            var sto = await _uow.Stolovi.GetAllAsync()
                .ContinueWith(t => t.Result.FirstOrDefault(s => s.BrojStola == dto.BrojStola));
            //ako ne postoji baci gresku
            if (sto == null) throw new KeyNotFoundException("Sto ne postoji.");
            //kreira novu rezervaciju
            var rez = new Rezervacija
            {
                Datum = dto.Datum,
                Vreme = dto.Vreme,
                BrojOsoba = dto.BrojOsoba,
                IDGosta = gost.IDGosta,
                IDStola = sto.IDStola,
                CreatedBy = dto.CreatedBy
            };

            await _uow.Rezervacije.AddAsync(rez);
            await _uow.SaveChangesAsync();
            return rez.IDRezervacije;
        }
        //azirira rezervaciju

        public async Task UpdateAsync(RezervacijaDto dto)
        {
            //trazi rezervaciju po idu
            var rez = await _uow.Rezervacije.GetByIdAsync(dto.IDRezervacije);
            //greska ako nepostoji
            if (rez == null) throw new KeyNotFoundException("Rezervacija ne postoji.");
            //proverava dali je sto zauzet tog dana
            bool zauzet = await _uow.Rezervacije
                .AnyAsync(r => r.IDStola == dto.BrojStola &&
                               r.Datum == dto.Datum &&
                               r.IDRezervacije != dto.IDRezervacije);
            //ako je zauzet baci gresku
            if (zauzet)
                throw new InvalidOperationException($"Sto {dto.BrojStola} je zauzet.");

            rez.Datum = dto.Datum;
            rez.Vreme = dto.Vreme;
            rez.BrojOsoba = dto.BrojOsoba;
            //proverava dali gost postoji

            var gost = await _uow.Gosti
                .FirstOrDefaultAsync(g => g.ImeGosta == dto.ImeGosta &&
                                          g.PrezimeGosta == dto.PrezimeGosta);
            //ako ne postoji pravi novog sebicnog gosta
            if (gost == null)
            {
                gost = new Gost
                {
                    ImeGosta = dto.ImeGosta,
                    PrezimeGosta = dto.PrezimeGosta
                };
                await _uow.Gosti.AddAsync(gost);
                await _uow.SaveChangesAsync();
            }
            //postavlja id gosta u rezervaciju

            rez.IDGosta = gost.IDGosta;

            var sto = await _uow.Stolovi.GetAllAsync()
                .ContinueWith(t => t.Result.FirstOrDefault(s => s.BrojStola == dto.BrojStola));

            if (sto == null) throw new KeyNotFoundException("Sto ne postoji.");

            rez.IDStola = sto.IDStola;

            await _uow.SaveChangesAsync();
        }
        //brisanje rezervacije
        public async Task DeleteAsync(int id)
        {
            var rez = await _uow.Rezervacije.GetByIdAsync(id);
            if (rez == null) throw new KeyNotFoundException("Rezervacija ne postoji.");

            await _uow.Rezervacije.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}