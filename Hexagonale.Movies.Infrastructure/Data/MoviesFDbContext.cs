using Hexagonale.Movies.Domain.Entities;
using Hexagonale.Movies.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Hexagonale.Movies.Infrastructure.Data
{
    public class MoviesFDbContext :  DbContext
    {
        //1- Constructeurs
        public MoviesFDbContext(DbContextOptions<MoviesFDbContext> options) : base(options)
        {
        }

        //2- Propriétes navigation
        public DbSet<Film> Films { get; set; }
        public DbSet<Favori> Favoris { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        //3- Méthodes de configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FavoriConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new UtilisateurConfiguration());

        }

    }
}
