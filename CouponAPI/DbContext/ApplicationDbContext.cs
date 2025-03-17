using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.DBContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; }
    }
}
