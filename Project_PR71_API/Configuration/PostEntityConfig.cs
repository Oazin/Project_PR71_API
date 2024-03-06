using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class PostEntityConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder) 
        {
            // Columns configuration 
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Title);
            builder.Property(e => e.Description);
            builder.Property(e => e.DateTime).IsRequired();

            // Primary key
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();

            // Relationship with Image
            builder.HasMany(e => e.Images)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Comment
            builder.HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Likes
            builder.HasMany(e => e.Likes)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
