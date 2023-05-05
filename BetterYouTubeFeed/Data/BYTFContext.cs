﻿using BetterYouTubeFeed.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BetterYouTubeFeed.Data;

public class BYTFContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Channel> Channels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // unsafe
        optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\My\\Desktop\\6 SEM\\PROGRAMOWANIE Z ANETKĄ\\BetterYT2\\BetterYouTubeFeed\\BetterYouTubeFeed\\YouTubeDatabase.mdf; Integrated Security = True; MultipleActiveResultSets = true");
    }

    public bool DropAccount(string accountId)
    {
        var account = this.Accounts.Where(c => c.AccountId == accountId);
        if (account.IsNullOrEmpty())
            return false;
        this.Accounts.Remove(account.First());
        return true;
    }
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
    public void AddAccount()
    {
        Accounts.Add(YouTubeDataAPI.GetAccountInfo(YouTubeDataAPI.Authenticate().Result));
    }
    public void UpdateChannels()
    {
        foreach (var account in Accounts)
        {
            YouTubeDataAPI.Authenticate(account.AuthId).Wait();
            foreach (var id in YouTubeDataAPI.GetSubsctiptionsID(account))
                if (this.Channels.Where(s => s.ChannelId == id).IsNullOrEmpty())
                    this.Channels.Add(YouTubeDataAPI.GetChannelInfo(account, id));
        }
        this.SaveChanges();
    }
    public void UpdateVideos()
    {
        foreach (var account in Accounts)
        {
            YouTubeDataAPI.Authenticate(account.AuthId).Wait();
            foreach (var channel in this.Channels)
                foreach (var video in YouTubeDataAPI.GetVideos(account, channel.ChannelId))
                    if (this.Videos.Where(v => v.VideoId == video.VideoId).IsNullOrEmpty())
                        this.Videos.Add(video);
        }
        this.SaveChanges();

    }
}   
