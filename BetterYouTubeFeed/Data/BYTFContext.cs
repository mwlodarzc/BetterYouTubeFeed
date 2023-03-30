using BetterYouTubeFeed.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterYouTubeFeed.Data;

public class BYTFContext : DbContext
{
    public DbSet<CommunityPost> CommunityPost { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Channel> Channels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // unsafe
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TestDatabaseApp;Trusted_Connection=True;TrustServerCertificate=True");
    }
}