using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYTF.Models;


public class Channel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public int Link { get; set; }

    public ICollection<Video> Videos { get; set; } = null!;
}