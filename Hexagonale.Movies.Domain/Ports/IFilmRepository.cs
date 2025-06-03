using Hexagonale.Movies.Domain.Entities;


namespace Hexagonale.Movies.Domain.Ports
{
    public interface IFilmRepository // Port secondaire
    {
        Task<Film> GetFilm(int filmId);
        Task<IEnumerable<Film>> GetFilms(IEnumerable<int> filmIds);
    }
}
