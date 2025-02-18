using EShop.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
