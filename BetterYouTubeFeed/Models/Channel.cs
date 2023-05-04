using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterYouTubeFeed.Models;


public class Channel
{
    [Required]
    public string ChannelId { get; set; }
    [Required]
    public string Name{ get; set;}
    [Required]
    public string CustomUrl{  get; set;}
    public string? Description { get; set; }
    public ICollection<CommunityPost>? ComunityPosts { get; set; }
    public ICollection<Video>? Videos { get; set; }
    public Channel()
    { }
    public Channel(string channelId, string name, string customUrl, string description)
    {
        this.ChannelId  = channelId;
        this.Name = name;
        this.CustomUrl = customUrl;
        this.Description = description;
    }
}