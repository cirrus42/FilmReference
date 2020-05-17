using FilmReference.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmReference.DataAccess.ConfigurationClasses
{
    public class GenreConfiguration : IEntityTypeConfiguration<GenreEntity>
    {
        public void Configure(EntityTypeBuilder<GenreEntity> builder)
        {
            builder.Property(e => e.Name)
                .IsUnicode(false);
            builder.Property(e => e.Description)
                .IsUnicode(false);
            builder.HasMany(e => e.Film)
                .WithOne(e => e.Genre)
                .HasForeignKey(e => e.GenreId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
