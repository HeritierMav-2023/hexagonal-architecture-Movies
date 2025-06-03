namespace Hexagonale.Movies.Domain.Entities
{
    public class Utilisateur
    {
        public int Id { get; private set; }
        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        // Navigation property : liste des favoris de l'utilisateur
        public ICollection<Favori> Favoris { get; set; } = new List<Favori>();

        // Constructeur
        protected Utilisateur() { } // Pour EF Core
        public Utilisateur(int id, string username, string email)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

    }
}
