using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class LikeEntityConfig : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            // Columns configuration 
            builder.Property(e => e.Id).IsRequired();

            // Primary key
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();

            // Relationship with User
            builder.HasOne(e => e.User)
                .WithMany(e => e.Likes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
