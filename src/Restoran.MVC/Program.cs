using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restoran.BLL.Mappings;
using Restoran.BLL.Services;
using Restoran.DAL;
using Restoran.DAL.Entities;
using Restoran.DAL.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(opts =>
        {
            opts.LoginPath = "/Account/Login";
            opts.AccessDeniedPath = "/Account/AccessDenied";
            opts.ExpireTimeSpan = TimeSpan.FromHours(2);
        });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());


builder.Services.AddDbContext<RestoranContext>(opts =>
    opts.UseSqlServer(
        "Server=(localdb)\\mssqllocaldb;Database=RestoranDB;Trusted_Connection=true;TrustServerCertificate=true",
        b => b.MigrationsAssembly("Restoran.DAL")));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRezervacijaService, RezervacijaService>();
builder.Services.AddScoped<IAuthService, AuthService>(); 


builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<RestoranContext>();
    ctx.Database.EnsureCreated();

    if (!ctx.Stolovi.Any())
    {
        var lokacije = new[] { "Prozor", "Sredina", "Kraj", "Ugao", "Terasa" };
        var rnd = new Random();
        for (int i = 1; i <= 20; i++)
            ctx.Stolovi.Add(new Sto { BrojStola = i, BrojMesta = 4, Lokacija = lokacije[rnd.Next(lokacije.Length)] });
        ctx.SaveChanges();
    }

    if (!ctx.Korisnici.Any())
    {
        ctx.Korisnici.AddRange(
            new Korisnik { Ime = "menadzer", Lozinka = "1234", Uloga = Uloga.Menadzer },
            new Korisnik { Ime = "gost", Lozinka = "1234", Uloga = Uloga.Gost });
        ctx.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rezervacija}/{action=Index}/{id?}");

app.Run();