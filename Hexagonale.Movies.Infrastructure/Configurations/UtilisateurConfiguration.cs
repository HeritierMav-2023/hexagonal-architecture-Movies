using Hexagonale.Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hexagonale.Movies.Infrastructure.Configurations
{
    public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
    {
        public void Configure(EntityTypeBuilder<Utilisateur> builder)
        {
            // Table name
            builder.ToTable("Utilisateurs");

            // Primary key
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Relations : Un utilisateur a plusieurs favoris
            builder.HasMany(u => u.Favoris)
                .WithOne()
                .HasForeignKey(f => f.UtilisateurId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
