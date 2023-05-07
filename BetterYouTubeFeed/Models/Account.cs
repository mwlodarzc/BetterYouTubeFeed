using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;

namespace BetterYouTubeFeed.Models;
/// <summary>
/// Entity Framework table definition
/// defines Account table class.
/// </summary>
public class Account
{
    [Required]
    public string AccountId { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public string AuthId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string FamilyName { get; set; }
    [Required]
    public string GivenName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    public string? CustomUrl { get; set; }

    public ICollection<Channel>? Channels { get; set; }

    public Account() { }
    public Account(string accountId,string type, string authId, string name, string familyName,string givenName, string email, string customUrl, string imageUrl)
    {
        this.AccountId = accountId;
        this.Type = type;
        this.AuthId = authId;
        this.Name = name;
        this.FamilyName = familyName;
        this.GivenName = givenName;
        this.Email = email;
        this.CustomUrl = customUrl;
        this.ImageUrl = imageUrl;
    }

}
