using LoanService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoanService.Data
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }

        public DbSet<Loan> Loans { get; set; }
    }
}
