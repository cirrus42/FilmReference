using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmReference.DataAccess
{
    public class PersonConfiguration : IEntityTypeConfiguration<DbClasses.PersonEntity>
    {
        public void Configure(EntityTypeBuilder<DbClasses.PersonEntity> builder)
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
