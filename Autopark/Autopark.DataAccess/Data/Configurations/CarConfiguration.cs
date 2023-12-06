using Autopark.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autopark.DataAccess.Data.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
	public void Configure(EntityTypeBuilder<Car> builder)
	{
		builder.HasIndex(car => car.LicenseNumber).IsUnique();	
	}

}