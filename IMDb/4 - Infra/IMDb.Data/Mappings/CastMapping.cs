using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Data.Mappings
{
    public class CastMapping : IEntityTypeConfiguration<Cast>
    {
        public void Configure(EntityTypeBuilder<Cast> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("CastId");

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.CastType)
                .IsRequired();

            builder.ToTable("Cast", "IMDb");

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.CascadeMode);
        }
    }
}
