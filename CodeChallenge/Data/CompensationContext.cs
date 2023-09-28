using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data
{
    public class CompensationContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compensation>().Navigation(c => c.Employee).AutoInclude();
        }


        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {

        }

        public DbSet<Compensation> Compensations { get; set; }
    }


}

