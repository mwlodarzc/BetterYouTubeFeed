using BetterYouTubeFeed.Data;
using BetterYouTubeFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace byt
{
    public partial class AddVideoPopup : Window
    {
        public AddVideoPopup()
        {
            InitializeComponent();
        }

        private void AddVideoButton(object sender, RoutedEventArgs e)
        {
            string title = VideoTitle.Text;
            string link = Link.Text;
            string UploadDate = VideoUploadDate.Text;
            string DownloadDate = VideoDownloadDate.Text;
            using (var context = new BYTFContext())
                 {
                var video = new Video(title, link, UploadDate, DownloadDate); ;
                    context.Videos.Add(video);
                    context.SaveChanges();
                    VideoTitle.Text = string.Empty;
                    VideoUploadDate.Text = string.Empty;
                    VideoDownloadDate.Text = string.Empty;
                    Link.Text = string.Empty;
                    MessageBox.Show("Umieszczono wpis w bazie danych!");
                 }
        }
    }
}