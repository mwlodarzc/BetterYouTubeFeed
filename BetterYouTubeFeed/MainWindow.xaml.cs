using BetterYouTubeFeed.Data;
using BetterYouTubeFeed.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace byt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BYTFContext db = new BYTFContext();

        public MainWindow()
        {
            InitializeComponent();
            this.Update_Displayed();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }
        private void AddVideoButton(object sender, RoutedEventArgs e)
        {
            AddVideoPopup infowindow = new AddVideoPopup();
            infowindow.Show();
        }


        private void AddChannel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged_Channels(object sender, SelectionChangedEventArgs e)
        {
            string name = (sender as ListBox).SelectedItem.ToString();
            string CustomUrl = db.Channels.Where(x => x.Name == name).Select(x => x.CustomUrl).SingleOrDefault();
            string URL = "https://www.youtube.com/" + CustomUrl;
            var defaultWebBrowser = new ProcessStartInfo
            {
                FileName = URL,
                UseShellExecute = true
            };
            Process.Start(defaultWebBrowser);

        }

        private void AddChannel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Channels_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update_Displayed()
        {
            Channels_ListBox.ItemsSource = db.Channels.Select(x => x.Name).ToList();
            Accounts_ListBox.ItemsSource = db.Accounts.Select(x=> x.Name).ToList();
            Videos_List.ItemsSource = db.Videos.ToList();
        }
        private void Channels_Button_Click(object sender, RoutedEventArgs e)
        {

            db.UpdateChannels();
            db.UpdateVideos();
            this.Update_Displayed();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OpenWebYt_Click(object sender, RoutedEventArgs e)
        {
            string VideoId = (e.Source as Button).Content.ToString();
            string URL = "https://www.youtube.com/watch?v=" + VideoId;
            var defaultWebBrowser = new ProcessStartInfo
            {
                FileName = URL,
                UseShellExecute = true
            };
            Process.Start(defaultWebBrowser);
        }

        private void DropDatabase_Click(object sender, RoutedEventArgs e)
        {
            db.Drop();
            this.Update_Displayed();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            db.AddAccount();
            db.UpdateChannels();
            db.UpdateVideos();
            this.Update_Displayed();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateChannels();
            db.UpdateVideos();
            this.Update_Displayed();
            var image = new BitmapImage(new Uri("/Application;Images\\watched.png", UriKind.Relative));
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
