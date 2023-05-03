using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterYouTubeFeed.Models;


public class Channel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? ChannelId { get; set; }
    [Required]
    public string? Name
    {
        get; set;
    }
    [Required]
    public string? Link
    {
        get; set;
    }

    public string Description { get; } = null!;

    public ICollection<CommunityPost> ComunityPosts { get; set; } = null!;

    public ICollection<Video> Videos { get; set; } = null!;

    public Channel()
    { }

    public Channel(string channelId, string name, string link, string description)
    {
        this.ChannelId = channelId;
        this.Name = name;
        this.Link = link;
        this.Description = description;
    }
}