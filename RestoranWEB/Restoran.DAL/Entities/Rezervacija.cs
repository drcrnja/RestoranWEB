using System;

namespace Restoran.DAL.Entities
{
    public class Rezervacija
    {
        public int IDRezervacije { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Vreme { get; set; }
        public int BrojOsoba { get; set; }

        public int IDGosta { get; set; }
        public Gost Gost { get; set; } = null!;

        public int IDStola { get; set; }
        public Sto Sto { get; set; } = null!;
        public int? NarudzbinaId { get; set; }  
        public Narudzbina? Narudzbina { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        
        
    }
}