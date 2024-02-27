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
            builder.Property(e => e.Picture).IsRequired().HasColumnType("bytea");
            builder.Property(e => e.Description);
            builder.Property(e => e.Like).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();

            // Primary key
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
        }
    }
}
