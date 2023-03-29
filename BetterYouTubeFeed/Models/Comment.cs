using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterYouTubeFeed.Models;
public class Comment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    public string Author { get; set; } = null!;
    [Required]
    public string VideoLink { get; set; } = null!;
    [Required]
    public DateTime TimeStamp { get; set; }
    public int? NumLikes { get; set; }
    public int? NumReplies { get; set; }

}
