using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Data.Mappings
{
    public class RatingOfMovieMapping : IEntityTypeConfiguration<RatingOfMovie>
    {
        public void Configure(EntityTypeBuilder<RatingOfMovie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("RatingOfMovieId");

            builder.Property(x => x.Rate)
                .IsRequired();

            builder.HasOne(x => x.Movie)
                .WithMany(m => m.RatingOfMovies)
                .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.User)
                .WithMany(m => m.RatingOfMovies)
                .HasForeignKey(x => x.UserId);

            builder.ToTable("RatingOfMovie", "IMDb");

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
