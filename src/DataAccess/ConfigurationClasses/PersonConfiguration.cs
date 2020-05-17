using FilmReference.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmReference.DataAccess.ConfigurationClasses
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.Property(e => e.FirstName)
                .IsUnicode(false);
            builder.Property(e => e.LastName)
                .IsUnicode(false);
            builder.Property(e => e.Description)
                .IsUnicode(false);
            builder.HasMany(e => e.FilmPerson)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
