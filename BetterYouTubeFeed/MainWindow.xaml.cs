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
        /// <summary>
        /// Database context for aplication window
        /// </summary>
        BYTFContext db = new BYTFContext();

        /// <summary>
        /// Constructor for MainWindow class objects.
        /// Initializes application window and updates listbox and listview displays
        /// </summary>
        /// 
        public MainWindow()
        {
            InitializeComponent();
            this.Update_Displayed();
        }

        /// <summary>
        /// Logic for selecting a specific channel.
        /// Function opens youtube.com with URL ending specific for selected Channel customURL
        /// </summary>
        /// <param name="sender">Selected channel</param>
        /// <param name="e">Event information</param>
        private void ListBox_SelectionChanged_Channels(object sender, SelectionChangedEventArgs e)
        {
            if (Channels_ListBox.Items.Count>0)
            {
                string name = ((sender as ListBox).SelectedItem as Channel).Name;
                string CustomUrl = db.Channels.Where(x => x.Name == name).Select(x => x.CustomUrl).SingleOrDefault();
                string URL = "https://www.youtube.com/" + CustomUrl;
                var defaultWebBrowser = new ProcessStartInfo
                {
                    FileName = URL,
                    UseShellExecute = true
                };
                Process.Start(defaultWebBrowser);
            }
        }

        /// <summary>
        /// Function that handles updates from database to UI.
        /// Connects Accounts, Channels and Videos lists to their database counterparts.
        /// </summary>
        private void Update_Displayed()
        {
            Channels_ListBox.ItemsSource = db.Channels.ToList();
            Accounts_ListBox.ItemsSource = db.Accounts.ToList();
            Videos_List.ItemsSource = db.Videos.ToList();
        }

        /// <summary>
        /// Function that handles oppening links to youtube video.
        /// After sellecting specifc video in video listview the function starts a browser process and directs it to the videos id.
        /// </summary>
        /// <param name="sender">Selected video</param>
        /// <param name="e">Event information</param>
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

        /// <summary>
        /// Deleting all entries in database for all tables and updating displays.
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void DropDatabase_Click(object sender, RoutedEventArgs e)
        {
            db.Drop();
            this.Update_Displayed();
            this.Refresh_Click(sender, e);
        }

        /// <summary>
        /// Adding an account to database.
        /// Function calls db.AddAccount and refreshes displays
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            db.AddAccount();
            this.Refresh_Click(sender, e);
        }

        /// <summary>
        /// Updates all listviews and listboxes from database.
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateChannels();
            db.UpdateVideos();
            this.Update_Displayed();
            var image = new BitmapImage(new Uri("/Application;Images\\watched.png", UriKind.Relative));
        }

        /// <summary>
        /// Minimizes aplication window
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        /// <summary>
        /// Closes aplication window
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// Handles aplication window moving
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Handles removal of specified account from Accounts Listbox.
        /// Removes the account from database with all related Channels and Videos.
        /// Updates displays
        /// </summary>
        /// <param name="sender">Selected button</param>
        /// <param name="e">Event information</param>
        private void Accounts_ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var Account = Accounts_ListBox.SelectedItem as Account;
            if (Account != null)
            {
                db.Accounts.Remove(Account);
                foreach (var channel in db.Channels.Where(c => c.AccountId == Account.AccountId))
                {
                    foreach (var video in db.Videos.Where(v=>v.ChannelId == channel.ChannelId))
                        db.Videos.Remove(video);
                    db.Channels.Remove(channel);
                }
            }
            this.Update_Displayed();
            this.Refresh_Click(sender,e);
        }
    }
}   
