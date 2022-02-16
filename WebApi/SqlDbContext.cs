using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext()
        {

        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }
        
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
    }
}
