using Microsoft.EntityFrameworkCore;
using CustomerService.Models;

namespace CustomerService.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		public DbSet<Customer> Customers { get; set; }
	}
}