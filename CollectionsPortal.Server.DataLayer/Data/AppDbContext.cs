using CollectionsPortal.Server.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectionsPortal.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}