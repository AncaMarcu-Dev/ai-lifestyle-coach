using Microsoft.EntityFrameworkCore;

namespace LifestyleCoach.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Goal> Goals { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
    }
}
