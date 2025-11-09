using Microsoft.EntityFrameworkCore;
using Core;

namespace DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Car> cars { get; set; }
    }
}
