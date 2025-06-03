using Hexagonale.Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hexagonale.Movies.Infrastructure.Configurations
{
    public class FavoriConfiguration : IEntityTypeConfiguration<Favori>
    {
        public void Configure(EntityTypeBuilder<Favori> builder)
        {
            builder.ToTable("Favoris");

            // Clé composite UtilisateurId + FilmId
            builder.HasKey(f => new { f.UtilisateurId, f.FilmId });

            // Propriétés
            builder.Property(f => f.DateAjout)
                .IsRequired();

            builder.Property(f => f.Vu)
                .IsRequired();

            builder.Property(f => f.DateVu)
                .IsRequired(false);

            // Relations
            builder.HasOne<Utilisateur>()
                .WithMany(u => u.Favoris)
                .HasForeignKey(f => f.UtilisateurId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Film>()
                .WithMany()
                .HasForeignKey(f => f.FilmId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
