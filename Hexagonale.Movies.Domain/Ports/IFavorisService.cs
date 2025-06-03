using Hexagonale.Movies.Domain.Entities;

namespace Hexagonale.Movies.Domain.Ports
{
    public interface IFavorisService // Port primaire
    {
        Task AjouterFavori(int utilisateurId, int filmId);
        Task RetirerFavori(int utilisateurId, int filmId);
        Task MarquerCommeVu(int utilisateurId, int filmId);
        Task<IEnumerable<Film>> ListerFavoris(int utilisateurId, string tri);
        Task<IEnumerable<Film>> ListerVus(int utilisateurId);
        Task<IEnumerable<Film>> ListerNonVus(int utilisateurId);
    }
}
