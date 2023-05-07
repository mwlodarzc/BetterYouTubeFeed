using BetterYouTubeFeed.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BetterYouTubeFeed.Data;
/// <summary>
/// Database context class definition.
/// </summary>
public class BYTFContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Channel> Channels { get; set; } = null!;

    /// <summary>
    /// Handles database connection
    /// </summary>
    /// <param name="optionsBuilder">Builder</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // unsafe
        string path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString();
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Michał\\Desktop\\tmp\\BetterYouTubeFeed\\BetterYouTubeFeed\\YouTubeDatabase.mdf;Integrated Security=True; MultipleActiveResultSets = true");
    }
    /// <summary>
    /// Handles database account table entry drop
    /// </summary>
    /// <param name="accountId">Dropped account</param>
    /// <returns>Execution boolean</returns>
    public bool DropAccount(string accountId)
    {
        var account = this.Accounts.Where(c => c.AccountId.Equals(accountId));
        if (account.IsNullOrEmpty())
            return false;
        this.Accounts.Remove(account.First());
        return true;
    }
    /// <summary>
    /// Handles database table cleaning.
    /// Removes all entries in all tables.
    /// </summary>
    public void Drop()
    {
        var accounts = from o in this.Accounts select o;
        foreach (var account in accounts)
            this.Accounts.Remove(account);
        var channels = from o in this.Channels select o;
        foreach (var channel in channels)
            this.Channels.Remove(channel);
        var videos = from o in this.Videos select o;
        foreach (var video in videos)
            this.Videos.Remove(video);
        this.SaveChanges();
    }
    /// <summary>
    /// Adds an account taken from YouTubeDataAPI class account info request.
    /// </summary>
    public void AddAccount()
    {
        Accounts.Add(YouTubeDataAPI.GetAccountInfo());
    }

    /// <summary>
    /// Updates Channels table with subscription of all accounts.
    /// </summary>
    public void UpdateChannels()
    {
        foreach (var account in Accounts)
            foreach (var id in YouTubeDataAPI.GetSubsctiptionsID(account))
                if (!this.Channels.Any(s => s.ChannelId == id))
                    this.Channels.Add(YouTubeDataAPI.GetChannelInfo(account, id));
        this.SaveChanges();
    }

    /// <summary>
    /// Updates Videos table for all subscriptions of all accounts.
    /// </summary>
    public void UpdateVideos()
    {
        if (!this.Accounts.IsNullOrEmpty())
        {
            Account tmp = this.Accounts.First();
            foreach (var channel in this.Channels)
                foreach (var video in YouTubeDataAPI.GetVideos(tmp, channel.ChannelId))
                    if (!this.Videos.Any(v => v.VideoId == video.VideoId))
                        this.Videos.Add(video);
            this.SaveChanges();
        }
    }
}   
