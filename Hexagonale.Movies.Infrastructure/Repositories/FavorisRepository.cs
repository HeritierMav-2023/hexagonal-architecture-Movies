using Hexagonale.Movies.Domain.Entities;
using Hexagonale.Movies.Domain.Ports;
using Hexagonale.Movies.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Hexagonale.Movies.Infrastructure.Repositories
{
    public class FavorisRepository : IFavorisRepository
    {

        //1- Propriétes pour accéder à la base de données ou au contexte
        private readonly MoviesFDbContext _context;

        //2. Constructeur pour initialiser les propriétés
        public FavorisRepository(MoviesFDbContext context) => _context = context;

        //3- Méthodes pour gérer les favoris
        public async Task AjouterFavori(Favori favori)
        {
            if(favori is null)
            {
                throw new ArgumentNullException(nameof(favori), "Le favori ne peut pas être null.");
            }
            await _context.Favoris.AddAsync(favori).AsTask();
        }
        //4- Méthodes pour retirer un favori
        public async Task RetirerFavori(int utilisateurId, int filmId)
        {
          
            if(utilisateurId <= 0 || filmId <= 0)
            {
                throw new ArgumentException("L'ID de l'utilisateur et l'ID du film doivent être supérieurs à zéro.");
            }
            var favori = await GetFavori(utilisateurId, filmId);
            if (favori == null)
            {
                throw new KeyNotFoundException("Favori non trouvé pour l'utilisateur et le film spécifiés.");
            }
            _context.Favoris.Remove(favori);
        }
        //5- Méthodes pour obtenir un favori spécifique ou tous les favoris d'un utilisateur
        public async Task<Favori> GetFavori(int utilisateurId, int filmId)
        {
            if(utilisateurId <= 0 || filmId <= 0)
            {
                throw new ArgumentException($"L'ID de l'utilisateur {utilisateurId} et l'ID du film {filmId} doivent être supérieurs à zéro.");
            }

            var favori = await _context.Favoris
                .FirstOrDefaultAsync(f => f.UtilisateurId == utilisateurId && f.FilmId == filmId);
            if (favori is  null)
                {
                throw new KeyNotFoundException("Favori non trouvé pour l'utilisateur et le film spécifiés.");
            }
            return favori;


        }
        //6- Méthodes pour obtenir tous les favoris d'un utilisateur
        public async Task<IEnumerable<Favori>> GetFavoris(int utilisateurId)
        {
            if(utilisateurId <= 0)
            {
                throw new ArgumentException("L'ID de l'utilisateur doit être supérieur à zéro.");
            }
            var favoris = await _context.Favoris
                .Where(f => f.UtilisateurId == utilisateurId)
                .ToListAsync();
            if (favoris is null || !favoris.Any())
                {
                throw new KeyNotFoundException("Aucun favori trouvé pour l'utilisateur spécifié.");
            }
            return favoris;
        }
        //7- Méthode pour sauvegarder les changements dans la base de données
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
    
}
