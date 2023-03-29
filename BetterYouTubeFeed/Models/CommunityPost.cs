using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BetterYouTubeFeed.Models;
public class CommunityPost
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    public DateTime TimeStamp { get; set; }
    [Required]
    public string? Link { get; set; } = null!;
    public string? Image { get; set; }

}
