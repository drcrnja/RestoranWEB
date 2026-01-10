using System.Security.Claims;
using Restoran.DAL.Entities;
using Restoran.DAL.UnitOfWork;

namespace Restoran.BLL.Services
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal?> LoginAsync(string ime, string lozinka);
        Task<bool> RegisterAsync(string ime, string lozinka);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        public AuthService(IUnitOfWork uow) => _uow = uow;
        
        public async Task<ClaimsPrincipal?> LoginAsync(string ime, string lozinka)
        {
            
            var k = await _uow.Korisnici.FirstOrDefaultAsync(
                x => x.Ime == ime && x.Lozinka == lozinka && x.Aktivan);
            if (k == null) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, k.Ime),
                new Claim(ClaimTypes.Role, k.Uloga.ToString())
            };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "RestoranCookie"));
        }

        public async Task<bool> RegisterAsync(string ime, string lozinka)
        {
            if (await _uow.Korisnici.FirstOrDefaultAsync(k => k.Ime == ime) != null)
                return false; // ime zauzeto
            var gost = new Korisnik
            {
                Ime = ime,
                Lozinka = lozinka,
                Uloga = Uloga.Gost,
                Aktivan = true
            };
            await _uow.Korisnici.AddAsync(gost);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}