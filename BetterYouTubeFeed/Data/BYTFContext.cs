using BetterYouTubeFeed.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Michał\\Desktop\\BetterYouTubeFeed\\BetterYouTubeFeed-main\\BetterYouTubeFeed\\YouTubeDatabase.mdf;Integrated Security=True");
    }
    public void UpdateChannels()
    {
        YouTubeDataAPI.Authenticate().Wait();
        foreach (var id in YouTubeDataAPI.GetSubsctiptionsID())
        {
            var channelCheck = from entry in this.Channels where entry.ChannelId == id select entry;
            if (channelCheck != null)
                continue;
            this.Channels.Add(YouTubeDataAPI.GetChannelInfo(id));
        }
    }
}
