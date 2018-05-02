using Microsoft.EntityFrameworkCore;

namespace wedding_planner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Guest> guests { get; set; }
        public DbSet<Wedding> weddings { get; set; }
    }
}