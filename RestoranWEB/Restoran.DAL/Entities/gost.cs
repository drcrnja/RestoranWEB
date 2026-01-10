using Restoran;

namespace Restoran.DAL.Entities
{
    //klasa za sebicnog gosta
    public class Gost
    {
        public int IDGosta { get; set; }
        public string ImeGosta { get; set; } = string.Empty;
        public string PrezimeGosta { get; set; } = string.Empty;
        public string? Telefon { get; set; }
        public string? Email { get; set; }

        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();
    }
}