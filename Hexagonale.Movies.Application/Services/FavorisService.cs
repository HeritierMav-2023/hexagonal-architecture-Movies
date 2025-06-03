using Hexagonale.Movies.Domain.Entities;
using Hexagonale.Movies.Domain.Ports;


namespace Hexagonale.Movies.Application.Services
{
    public class FavorisService : IFavorisService
    {
        //1- propriétes privées pour les dépôts
        private readonly IFavorisRepository _favorisRepo;
        private readonly IFilmRepository _filmRepo;

        //2- constructeur pour injecter les dépôts
        public FavorisService(IFavorisRepository favorisRepo, IFilmRepository filmRepo)
        {
            _favorisRepo = favorisRepo;
            _filmRepo = filmRepo;
        }

        //3- implémentation des méthodes du service
        public async Task AjouterFavori(int utilisateurId, int filmId)
        {
            var favori = new Favori
            {
                UtilisateurId = utilisateurId,
                FilmId = filmId,
                Vu = false,
                DateAjout = DateTime.UtcNow
            };
            await _favorisRepo.AjouterFavori(favori);
            await _favorisRepo.SaveChangesAsync();
        }
        //4- méthode pour retirer un film des favoris
        public async Task RetirerFavori(int utilisateurId, int filmId)
        {
            await _favorisRepo.RetirerFavori(utilisateurId, filmId);
            await _favorisRepo.SaveChangesAsync();
        }

        //5- méthode pour marquer un film comme vu
        public async Task MarquerCommeVu(int utilisateurId, int filmId)
        {
            var favori = await _favorisRepo.GetFavori(utilisateurId, filmId);
            if (favori == null) throw new ArgumentException("Favori introuvable");
            favori.Vu = true;
            favori.DateVu = DateTime.UtcNow;
            await _favorisRepo.SaveChangesAsync();
        }

        //6- méthode pour lister les films favoris de l'utilisateur avec tri
        public async Task<IEnumerable<Film>> ListerFavoris(int utilisateurId, string tri)
        {
            var favoris = await _favorisRepo.GetFavoris(utilisateurId);
            var films = await _filmRepo.GetFilms(favoris.Select(f => f.FilmId));
            return tri switch
            {
                "date" => films.OrderByDescending(f => f.DateSortie),
                "note" => films.OrderByDescending(f => f.NoteGlobale),
                _ => films
            };
        }

        //7- méthode pour lister les films vus par l'utilisateur
        public async Task<IEnumerable<Film>> ListerVus(int utilisateurId)
        {
            var favoris = await _favorisRepo.GetFavoris(utilisateurId);
            var vus = favoris.Where(f => f.Vu).Select(f => f.FilmId);
            return await _filmRepo.GetFilms(vus);
        }

        //8- méthode pour lister les films non vus par l'utilisateur
        public async Task<IEnumerable<Film>> ListerNonVus(int utilisateurId)
        {
            var favoris = await _favorisRepo.GetFavoris(utilisateurId);
            var nonVus = favoris.Where(f => !f.Vu).Select(f => f.FilmId);
            return await _filmRepo.GetFilms(nonVus);
        }
    }

}
