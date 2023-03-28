using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYTF.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Link { get; set; }

        public string? UploadDate { get; set; }
        public string? DownloadDate { get; set; }
    }
}