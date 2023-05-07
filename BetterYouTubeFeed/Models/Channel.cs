using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterYouTubeFeed.Models;

/// <summary>
/// Entity Framework table definition
/// defines Channel table class.
/// </summary>
public class Channel
{
    [Required]
    public string ChannelId { get; set; }
    [Required]
    public string Name{ get; set;}
    [Required]
    public string CustomUrl {  get; set;}
    [Required]
    public string ImageUrl { get; set;}
    public string? Description { get; set; }
    public string? AccountId { get; set;}
    public ICollection<Video>? Videos { get; set; }
    public Channel()
    { }
    public Channel(string channelId, string name, string customUrl,string imageUrl, string description, string ytaccountId)
    {
        this.ChannelId  = channelId;
        this.Name = name;
        this.CustomUrl = customUrl;
        this.ImageUrl = imageUrl;
        this.Description = description;
        this.AccountId = ytaccountId;
    }
}