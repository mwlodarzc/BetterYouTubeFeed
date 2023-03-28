using BYTF.Models;

using Microsoft.EntityFrameworkCore;

namespace BYTF.Data;

public class BYTFContext : DbContext
{
    public DbSet<Channel> Channels { get; set; } = null!;

    public DbSet<Video> Videos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // unsafe
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"G:\\My Drive\\Semestr 6\\PlatformyProg.NetiJava\\BetterYouTubeFeed\\BetterYouTubeFeed\\YouTube.mdf\";Integrated Security=True");
    }
}