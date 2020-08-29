using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Data.Mappings
{
    public class CastOfMovieMapping : IEntityTypeConfiguration<CastOfMovie>
    {
        public void Configure(EntityTypeBuilder<CastOfMovie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("CastOfMovieId");

            builder.HasOne(x => x.Movie)
                .WithMany(m => m.CastOfMovies)
                .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.Cast)
                .WithMany(u => u.CastOfMovies)
                .HasForeignKey(x => x.CastId);

            builder.ToTable("CastOfMovie", "IMDb");

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
