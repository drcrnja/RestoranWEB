using System;
using System.ComponentModel.DataAnnotations;

namespace Restoran.BLL.DTOs
{
    public class RezervacijaDto
    {
        public int IDRezervacije { get; set; }

        [Required(ErrorMessage = "Datum je obavezan.")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Vreme je obavezno.")]
        public TimeSpan Vreme { get; set; }

        public int BrojOsoba { get; set; }

        [Required(ErrorMessage = "Ime gosta je obavezno.")]
        public string ImeGosta { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime gosta je obavezno.")]
        public string PrezimeGosta { get; set; } = string.Empty;
        public int BrojStola { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string? NarudzbinaProizvod { get; set; }

    }
}