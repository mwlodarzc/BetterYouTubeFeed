using BetterYouTubeFeed.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.CompilerServices;

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
        optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Michał\\Desktop\\BetterYouTubeFeed\\BetterYouTubeFeed\\YouTubeDatabase.mdf; Integrated Security = True; MultipleActiveResultSets = true");
    }
    public void Drop()
    {
        var channels = from o in this.Channels select o;
        foreach (var channel in channels)
            this.Channels.Remove(channel);
        var videos = from o in this.Videos select o;
        foreach (var video in videos)
            this.Videos.Remove(video);
        this.SaveChanges();
    }
    public void UpdateChannels()
    {
        YouTubeDataAPI.Authenticate().Wait();
        foreach (var id in YouTubeDataAPI.GetSubsctiptionsID())
            if (this.Channels.Where(s => s.ChannelId == id).IsNullOrEmpty())
                this.Channels.Add(YouTubeDataAPI.GetChannelInfo(id));
        this.SaveChanges();
    }
    public void UpdateVideos()
    {
        YouTubeDataAPI.Authenticate().Wait();
        foreach (var channel in this.Channels)
            foreach (var video in YouTubeDataAPI.GetVideos(channel.ChannelId))
                if (this.Videos.Where(v => v.VideoId == video.VideoId).IsNullOrEmpty())
                    this.Videos.Add(video);
        this.SaveChanges();

    }
}   
