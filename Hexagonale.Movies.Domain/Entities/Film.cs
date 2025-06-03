namespace Hexagonale.Movies.Domain.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; } = null!;
        public DateTime DateSortie { get; set; }
        public float NoteGlobale { get; set; }
    }
}
