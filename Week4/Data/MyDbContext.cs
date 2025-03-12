using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Week4.Models.Entities;

namespace Week4.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Trusted_Connection=True; Encrypt=True; TrustServerCertificate = True;");
        //}

        public DbSet<Product> Products { get; set; }
    }
}
