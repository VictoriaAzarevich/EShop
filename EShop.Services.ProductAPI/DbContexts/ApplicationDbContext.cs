using EShop.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
    }
}
