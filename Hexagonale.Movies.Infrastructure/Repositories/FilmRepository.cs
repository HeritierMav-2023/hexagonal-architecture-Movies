using Hexagonale.Movies.Domain.Entities;
using Hexagonale.Movies.Domain.Ports;
using Hexagonale.Movies.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Hexagonale.Movies.Infrastructure.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        //1- Propriétes pour accéder à la base de données ou au contexte
        private readonly MoviesFDbContext _context;

        //2. Constructeur pour initialiser les propriétés
        public FilmRepository(MoviesFDbContext context) => _context = context;

        //3. Méthodes pour interagir avec la base de données
        public async Task<Film> GetFilm(int filmId)
        {
            if(filmId <= 0)
            {
                throw new ArgumentException("L'ID du film doit être supérieur à zéro.", nameof(filmId));
            }
            var film = await _context.Films.FindAsync(filmId);
            if (film == null)
            {
                throw new KeyNotFoundException($"Film avec l'ID {filmId} non trouvé.");
            }
            return film;
        }

        // Récupère une liste de films par leurs IDs
        public async Task<IEnumerable<Film>> GetFilms(IEnumerable<int> filmIds)
        {
            if(filmIds == null || !filmIds.Any())
            {
                throw new ArgumentException("La liste des IDs de films ne peut pas être vide.", nameof(filmIds));
            }
            var films = await _context.Films
                .Where(f => filmIds.Contains(f.Id))
                .ToListAsync();
            if (films == null || !films.Any())
                {
                throw new KeyNotFoundException("Aucun film trouvé pour les IDs spécifiés.");
            }
            return films;
        }
    }
}
