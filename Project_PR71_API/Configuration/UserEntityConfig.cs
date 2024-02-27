using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Columns configuration 
            builder.Property(e => e.email).IsRequired();
            builder.Property(e => e.Picture).IsRequired().HasColumnType("bytea");
            builder.Property(e => e.Username);
            builder.Property(e => e.Bio);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Fistname).IsRequired();
            
            // Primary key 
            builder.HasKey(e => e.email);
            builder.HasIndex(e => e.email).IsUnique();

            // Relationship with Post
            builder.HasMany(e => e.Posts)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
