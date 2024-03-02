using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_PR71_API.Models;

namespace Project_PR71_API.Configuration
{
    public class FollowEntityConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            // Primary key 
            builder.HasKey(e => new { e.FollowingEmail, e.FollowerEmail });
            builder.HasIndex(e => new { e.FollowingEmail, e.FollowerEmail }).IsUnique();

            // Relationship with Follower
            builder.HasOne(uf => uf.Follower)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.FollowerEmail)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Follow
            builder.HasOne(uf => uf.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowingEmail)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
