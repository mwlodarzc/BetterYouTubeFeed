using BetterYouTubeFeed.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Michał\\Desktop\\BetterYouTubeFeed\\BetterYouTubeFeed\\YouTubeDatabase.mdf;Integrated Security=True;MultipleActiveResultSets=true");
    }
    public void UpdateChannels()
    {
        YouTubeDataAPI.Authenticate().Wait();
        foreach (var id in YouTubeDataAPI.GetSubsctiptionsID())
            if (this.Channels.Where(s => s.ChannelId == id).IsNullOrEmpty())
                this.Channels.Add(YouTubeDataAPI.GetChannelInfo(id));
    }
    public void UpdateVideos()
    {
        YouTubeDataAPI.Authenticate().Wait();
        foreach (var channel in this.Channels)
            foreach (var video in YouTubeDataAPI.GetVideos(channel.ChannelId))
                if (this.Videos.Where(v => v.VideoId == video.VideoId).IsNullOrEmpty())
                    this.Videos.Add(video);
    }
}   
