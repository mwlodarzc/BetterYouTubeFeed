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
        [Required]
        public string? UploadDate { get; set; }
        [Required]
        public string? DownloadDate { get; set; }

        public Video(int id, string? title, string? link, string? uploadDate, string? downloadDate)
        {
            Title = title;
            Link = link;
            UploadDate = uploadDate;
            DownloadDate = downloadDate;
        }
    }
}