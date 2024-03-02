using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class CommentEntityConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            // Columns configuration 
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Content).IsRequired();

            // Primary key
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();

            // Relationship with User
            builder.HasOne(e => e.Writer)
                .WithMany(e => e.Comments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
