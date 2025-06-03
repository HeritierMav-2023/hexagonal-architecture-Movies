using Hexagonale.Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hexagonale.Movies.Infrastructure.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Titre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.DateSortie)
                .IsRequired();

            builder.Property(f => f.NoteGlobale)
                .IsRequired();


        }
    }
}
