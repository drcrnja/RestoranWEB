using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restoran.BLL.DTOs;
using Restoran.BLL.Services;
using Restoran.DAL.UnitOfWork;
using System.Security.Claims;

namespace Restoran.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IUnitOfWork _uow;

        public AccountController(IAuthService auth, IUnitOfWork uow)
        {
            _auth = auth;
            _uow = uow;
        }

        // ---------- PRIJAVA ----------
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var principal = await _auth.LoginAsync(dto.Ime, dto.Lozinka);
            if (principal == null)
            {
                ModelState.AddModelError("", "E nece da moze.Neispravni podaci ili si blokiran sine");
                return View(dto);
            }

           
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            
            var role = principal.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Kuvar")
                return RedirectToAction("Index", "Narudzbina");

            return RedirectToAction("Index", "Rezervacija");
        }

       
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            bool ok = await _auth.RegisterAsync(dto.Ime, dto.Lozinka);
            if (!ok)
            {
                ModelState.AddModelError("", "Ime je već zauzeto.");
                return View(dto);
            }

            return RedirectToAction(nameof(Login));
        }

        // ---------- ODJAVA ----------
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        // ---------- PRISTUP ODBIJEN ----------
        public IActionResult AccessDenied() => View();

        // ---------- LISTA KORISNIKA (samo Menadžer) ----------
        [Authorize(Roles = "Menadzer")]
        public async Task<IActionResult> ListaKorisnika()
        {
            var korisnici = await _uow.Korisnici.GetAllAsync();
            return View(korisnici);
        }

        // ---------- BLOKIRAJ / AKTIVIRAJ ----------
        [HttpPost]
        [Authorize(Roles = "Menadzer")]
        public async Task<IActionResult> PromeniStatus(int id)
        {
            var k = await _uow.Korisnici.FirstOrDefaultAsync(x => x.Id == id);
            if (k != null)
            {
                k.Aktivan = !k.Aktivan;
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ListaKorisnika));
        }
    }
}