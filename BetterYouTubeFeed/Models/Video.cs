using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterYouTubeFeed.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Link { get; set; }

        public string? UploadDate { get; set; }
        public string? DownloadDate { get; set; }
    }
}