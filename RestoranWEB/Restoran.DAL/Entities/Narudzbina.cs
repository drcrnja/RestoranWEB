using System;

namespace Restoran.DAL.Entities
{
    public class Narudzbina
    {
        public int Id { get; set; }
        public int BrojStola { get; set; }
        public string Proizvod { get; set; } = string.Empty;
        public DateTime Datum { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;

        // veza ka rezervaciji 
        public int? RezervacijaId { get; set; }
        public Rezervacija? Rezervacija { get; set; }
    }
}