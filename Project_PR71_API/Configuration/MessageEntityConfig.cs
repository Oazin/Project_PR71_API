using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class MessageEntityConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            // Columns configuration 
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Content).IsRequired();

            // Primary key
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();

            // Relationship with Sender
            builder.HasOne(e => e.Sender)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Receiver
            builder.HasOne(e => e.Receiver)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
