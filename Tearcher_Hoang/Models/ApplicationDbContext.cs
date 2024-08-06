using Microsoft.EntityFrameworkCore;

namespace Tearcher_Hoang.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<NhaSanXuat> nhaSanXuats { get; set; }
    }
}
