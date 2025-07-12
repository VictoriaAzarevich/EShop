using EShop.Email.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Email.DbContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
