using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class StoryEntityConfig : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            // Columns configuration 
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Image).IsRequired().HasColumnType("bytea");
            builder.Property(e => e.DateTime).IsRequired();

            // Primary key
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
        }
    }
}
