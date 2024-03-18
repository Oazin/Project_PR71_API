using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class ChatEntityConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            // Columns configuration 
            builder.Property(e => e.Id).IsRequired();

            // Primary key 
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();

            // Relationship with Post
            builder.HasMany(e => e.Messages)
                .WithOne(e => e.Chat)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
