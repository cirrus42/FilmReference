using FilmReference.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmReference.DataAccess.ConfigurationClasses
{
    public class StudioConfiguration : IEntityTypeConfiguration<StudioEntity>
    {
        public void Configure(EntityTypeBuilder<StudioEntity> builder)
        {
            builder.Property(e => e.Name)
                .IsUnicode(false);
            builder.Property(e => e.Description)
                .IsUnicode(false);
            builder.HasMany(e => e.Film)
                .WithOne(e => e.Studio)
                .HasForeignKey(e => e.StudioId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
