using btth01.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
namespace btth01.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<view_Post_Menu> PostMenus { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
    }
}
