using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(user => user.Email).IsUnique();
		builder.HasIndex(user => user.PhoneNumber).IsUnique();
		builder.HasIndex(user => user.UserName).IsUnique();
	}

}
