using BetterYouTubeFeed.Data;
using BetterYouTubeFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace byt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string>? ch;
        BYTFContext db = new BYTFContext();

        public MainWindow()
        {
            InitializeComponent();
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
        private void AddChannel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Channels_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Channels_Button_Click(object sender, RoutedEventArgs e)
        {

            db.UpdateChannels();
            db.SaveChanges();
            Channels_ListBox.ItemsSource = db.Channels.Select(x => x.Name).ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
