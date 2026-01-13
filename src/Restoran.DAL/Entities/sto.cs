using Restoran.DAL.Entities;

namespace Restoran.DAL.Entities
    //klasa za sto
{
    public class Sto
    {
        public int IDStola { get; set; }
        public int BrojStola { get; set; }
        public int BrojMesta { get; set; }
        public string? Lokacija { get; set; }

        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();
    }
}