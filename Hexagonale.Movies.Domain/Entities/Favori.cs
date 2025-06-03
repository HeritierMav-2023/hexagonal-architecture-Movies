namespace Hexagonale.Movies.Domain.Entities
{
    public class Favori
    {
        public int UtilisateurId { get; set; }
        public int FilmId { get; set; }
        public bool Vu { get; set; }
        public DateTime? DateVu { get; set; }
        public DateTime DateAjout { get; set; }
    }
}
