using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterYouTubeFeed.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string UploadDate { get; set; }
        [Required]
        public string DownloadDate { get; set; }

        public Video(){}
        public Video(string title, string videoId, string uploadDate, string downloadDate)
        {
            Title = title;
            VideoId = videoId;
            UploadDate = uploadDate;
            DownloadDate = downloadDate;
        }
    }
}