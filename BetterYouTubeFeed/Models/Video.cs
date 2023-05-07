using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterYouTubeFeed.Models
{
    public class Video
    {
        [Required]
        public string VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string UploadDate { get; set; }
        [Required]
        public string DownloadDate { get; set; }
        [Required]
        public string ThumbnailLink { get; set; }
        public string? ChannelId { get; set; }
        public bool watched { get; set; }
        public Video(){}
        public Video(string videoId, string title, string uploadDate, string downloadDate, string thumbnailLink, string? channelId)
        {
            this.VideoId = videoId;
            this.Title = title;
            this.UploadDate = uploadDate;
            this.DownloadDate = downloadDate;
            this.ThumbnailLink = thumbnailLink;
            this.ChannelId = channelId;
            this.watched = false;
        }
    }
}