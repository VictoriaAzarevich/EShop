using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.DBContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
