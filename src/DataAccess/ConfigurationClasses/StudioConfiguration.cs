﻿using FilmReference.DataAccess.DbClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmReference.DataAccess
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
