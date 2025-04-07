using Microsoft.EntityFrameworkCore;
using Transaction.Models;

namespace Transaction.Data
{
    public class TransContext : DbContext
    {
        public TransContext(DbContextOptions<TransContext> options)
        : base(options) { }

        public DbSet<Transactions> Transactions { get; set; }

    }
}
