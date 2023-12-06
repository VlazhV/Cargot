using Autopark.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autopark.DataAccess.Data.Configurations;

public class TrailerConfiguration : IEntityTypeConfiguration<Trailer>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Trailer> builder)
    {
        builder.HasIndex(trailer => trailer.LicenseNumber).IsUnique();
    }

}