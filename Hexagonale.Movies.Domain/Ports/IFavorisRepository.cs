using Hexagonale.Movies.Domain.Entities;

namespace Hexagonale.Movies.Domain.Ports
{
    public interface IFavorisRepository // Port secondaire
    {
        Task AjouterFavori(Favori favori);
        Task RetirerFavori(int utilisateurId, int filmId);
        Task<Favori> GetFavori(int utilisateurId, int filmId);
        Task<IEnumerable<Favori>> GetFavoris(int utilisateurId);
        Task SaveChangesAsync();
    }

}
