using Hexagonale.Movies.Domain.Entities;
using Hexagonale.Movies.Infrastructure.Data;


namespace Hexagonale.Movies.Infrastructure.SeedData
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(MoviesFDbContext context)
        {
            // --- 1. Création de quelques utilisateurs ---
            var utilisateurs = new List<Utilisateur>
            {
                new Utilisateur(1,"Alice","alice@mail.com" ),
                new Utilisateur (2, "Bob", "bob@mail.com")
            };

                // --- 2. Création d'une dizaine de films ---
                var films = new List<Film>
            {
                new Film { Titre = "Inception", DateSortie = new DateTime(2010, 7, 16), NoteGlobale = 8.8f },
                new Film { Titre = "Interstellar", DateSortie = new DateTime(2014, 11, 7), NoteGlobale = 8.6f },
                new Film { Titre = "The Matrix", DateSortie = new DateTime(1999, 3, 31), NoteGlobale = 8.7f },
                new Film { Titre = "The Godfather", DateSortie = new DateTime(1972, 3, 24), NoteGlobale = 9.2f },
                new Film { Titre = "Pulp Fiction", DateSortie = new DateTime(1994, 10, 14), NoteGlobale = 8.9f },
                new Film { Titre = "Forrest Gump", DateSortie = new DateTime(1994, 7, 6), NoteGlobale = 8.8f },
                new Film { Titre = "Fight Club", DateSortie = new DateTime(1999, 10, 15), NoteGlobale = 8.8f },
                new Film { Titre = "The Dark Knight", DateSortie = new DateTime(2008, 7, 18), NoteGlobale = 9.0f },
                new Film { Titre = "Gladiator", DateSortie = new DateTime(2000, 5, 5), NoteGlobale = 8.5f },
                new Film { Titre = "Avatar", DateSortie = new DateTime(2009, 12, 18), NoteGlobale = 7.8f }
            };

            //---3.Création de favoris(certains vus, d'autres non vus) ---
           

            var favoris = new List<Favori>
            {
                // Alice a vu Inception et Interstellar, et a ajouté Matrix non vu
                new Favori { UtilisateurId = 1, FilmId = 1, Vu = true, DateVu = DateTime.UtcNow.AddDays(-10), DateAjout = DateTime.UtcNow.AddDays(-20) },
                new Favori { UtilisateurId = 1, FilmId = 2, Vu = true, DateVu = DateTime.UtcNow.AddDays(-5), DateAjout = DateTime.UtcNow.AddDays(-15) },
                new Favori { UtilisateurId = 1, FilmId = 3, Vu = false, DateVu = null, DateAjout = DateTime.UtcNow.AddDays(-2) },

                // Bob a vu The Godfather, Fight Club et ajouté Gladiator non vu
                new Favori { UtilisateurId = 2, FilmId = 4, Vu = true, DateVu = DateTime.UtcNow.AddDays(-30), DateAjout = DateTime.UtcNow.AddDays(-40) },
                new Favori { UtilisateurId = 2, FilmId = 5, Vu = true, DateVu = DateTime.UtcNow.AddDays(-1), DateAjout = DateTime.UtcNow.AddDays(-3) },
                new Favori { UtilisateurId = 2, FilmId = 6, Vu = false, DateVu = null, DateAjout = DateTime.UtcNow.AddDays(-1) }
            };

            // --- 4. Ajout à la base si non existant ---
            if (!context.Utilisateurs.Any())
                context.Utilisateurs.AddRange(utilisateurs);

            if (!context.Films.Any())
                context.Films.AddRange(films);

            if (!context.Favoris.Any())
                context.Favoris.AddRange(favoris);

            await context.SaveChangesAsync();
        }
    }

}
