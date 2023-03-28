﻿using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYTF.Models;


public class Channel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Link { get; set; }

    public string Description { get; set; } = null!;
    
    public ICollection<Video> Videos { get; set; } = null!;
}