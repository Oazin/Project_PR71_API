using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<Follow> Follow { get; set; }

        public DbSet<Like> Like { get; set; }

        public DbSet<SavePost> SavePost { get; set; }

        public DbSet<Chat> Chat { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfig());
            modelBuilder.ApplyConfiguration(new PostEntityConfig());
            modelBuilder.ApplyConfiguration(new ImageEntityConfig());
            modelBuilder.ApplyConfiguration(new CommentEntityConfig());
            modelBuilder.ApplyConfiguration(new MessageEntityConfig());
            modelBuilder.ApplyConfiguration(new FollowEntityConfig());
            modelBuilder.ApplyConfiguration(new LikeEntityConfig());
            modelBuilder.ApplyConfiguration(new SavePostEntityConfig());
            modelBuilder.ApplyConfiguration(new ChatEntityConfig());
        }

    }
}