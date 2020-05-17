using FilmReference.DataAccess.ConfigurationClasses;
using FilmReference.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmReference.DataAccess
{
    public class FilmReferenceContext : DbContext
    {
        public FilmReferenceContext(DbContextOptions<FilmReferenceContext> options) : base(options) {}

        public DbSet<FilmEntity> Film { get; set;}
        public DbSet<FilmPersonEntity> FilmPerson { get; set; }
        public DbSet<GenreEntity> Genre { get; set; }
        public DbSet<PersonEntity> Person { get; set; }
        public DbSet<StudioEntity> Studio { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FilmConfiguration());
            builder.ApplyConfiguration(new FilmPersonConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new StudioConfiguration());
        }
    }
}