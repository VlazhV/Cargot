using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.DataAccess.Data;

public class DatabaseContext: IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
	
}