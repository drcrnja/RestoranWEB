# 🍽️ Restoran – Rezervacija stolova i poručivanje

ASP.NET Core MVC aplikacija za **rezervaciju stolova** i **poručivanje hrane** u restoranu, razvijena u okviru predmeta **Arhitektura informacionih sistema** – Tehnički fakultet „Mihajlo Pupin“ Zrenjanin.

---

## 🚀 Tehnologije

- **ASP.NET Core 10** (MVC)
- **Entity Framework Core 10** – Code First
- **SQL Server** (LocalDB)
- **AutoMapper**
- **Bootstrap 5**
- **ASP.NET Core Identity** (cookie autentifikacija)
- **Git & GitHub**

---

## 📋 Funkcionalnosti

| Uloga        | Dozvoljeno                                                                 |
|--------------|------------------------------------------------------------------------------|
| **Gost**     | rezerviše svoje stolo­ve, poručuje hranu, pregleda samo svoje podatke        |
| **Konobar**  | pregled **svih** rezervacija i narudžbina, ažurira statuse                  |
| **Kuvar**    | pregled **svih** narudžbina – **ne ulazi u rezervacije**                    |
| **Menadžer** | blokira/aktivira **goste**, upravlja korisnicima, pregleda sve izveštaje    |

---

### Project Architecture

To ensure enterprise-grade scalability, the project is structured into three distinct layers:

1. **MVC (Model, View & Controller) :** Handles the UI logic, Razor Pages, and Controller-to-View communication.
2. **BLL (Business Logic Layer):** Has the core services (ex. `RezervacijaServices`) and interfaces that calculate standings and enforce tournament rules.
3. **DAL (Data Access Layer):** Manages the `DbContext`, migrations, and the overall data in the web application.



### HOW TO START

1. **Clone the repository:**
```bash
git clone https://github.com/drcrnja/RestoranWEB

```

2. **Configure the Database:**
Update the connection string in `appsettings.json`, then run the following in the Package Manager Console:
```powershell
Update-Database

```

3. **Run the App:**
Press **F5** in Visual Studio or use the CLI:
```bash
dotnet run

```

###  Admin Access
After setting up the database, you can use these credentials to access the administrative features:

| Role | Email | Password |
| :--- | :--- | :--- |
| **Administrator** | `menadzer` | `1234` |

## License

[![License: CC BY-NC-SA 4.0](https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg)](http://creativecommons.org/licenses/by-nc-sa/4.0/)

This work is licensed under a [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License](http://creativecommons.org/licenses/by-nc-sa/4.0/).

Full license: [LICENSE](LICENSE.txt) | [CC BY-NC-SA 4.0](https://creativecommons.org/licenses/by-nc-sa/4.0/)
**Copyright © 2026 Tomic Vasilije**
