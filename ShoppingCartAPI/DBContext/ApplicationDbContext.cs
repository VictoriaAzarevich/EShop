using Microsoft.EntityFrameworkCore;

namespace ShoppingCartAPI.DBContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
    }
}
