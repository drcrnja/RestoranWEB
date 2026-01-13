using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restoran.DAL.Entities;
using Restoran.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Restoran.MVC.Controllers
{
    [Authorize]
    public class NarudzbinaController : Controller
    {
        private readonly IUnitOfWork _uow;
        public NarudzbinaController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index()
        {
            IEnumerable<Narudzbina> model;

            if (User.IsInRole("Menadzer") || User.IsInRole("Konobar"))
                model = await _uow.Narudzbine.GetAllAsync();
            else if (User.IsInRole("Kuvar"))
                model = await _uow.Narudzbine.GetAllAsync();
            else
                model = await _uow.Narudzbine.GetByUserAsync(User.Identity?.Name ?? string.Empty);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create([FromQuery] int sto)
        {
            ViewBag.Sto = sto;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Narudzbina dto)
        {
            dto.CreatedBy = User.Identity?.Name ?? string.Empty;
            if (!ModelState.IsValid) return View(dto);

            await _uow.Narudzbine.AddAsync(dto);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int sto)
        {
            var n = await _uow.Narudzbine.FirstOrDefaultAsync((Narudzbina x) =>
                        x.BrojStola == sto &&
                        x.CreatedBy == User.Identity!.Name);
            if (n == null) return NotFound();
            return View(n);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Narudzbina dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var n = await _uow.Narudzbine.FirstOrDefaultAsync((Narudzbina x) =>
                        x.Id == dto.Id &&
                        x.CreatedBy == User.Identity!.Name);
            if (n == null) return NotFound();

            n.Proizvod = dto.Proizvod;
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var n = await _uow.Narudzbine.GetByIdAsync(id);
            if (n == null) return NotFound();

            _uow.Narudzbine.DeleteAsync(n);  
            await _uow.SaveChangesAsync();    
            return RedirectToAction(nameof(Index));
        }
    }
}