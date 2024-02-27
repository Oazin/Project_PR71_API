using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Configuration;

namespace Project_PR71_API.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<Post> Post { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfig());
            modelBuilder.ApplyConfiguration(new PostEntityConfig());
        }

    }
}