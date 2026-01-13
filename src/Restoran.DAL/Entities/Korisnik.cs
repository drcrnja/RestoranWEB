namespace Restoran.DAL.Entities
{
    public enum Uloga { Menadzer = 0, Kuvar = 1, Konobar = 2, Gost = 3 }

    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Lozinka { get; set; } = string.Empty;
        public Uloga Uloga { get; set; }
        public bool Aktivan { get; set; } = true;
    }
}
