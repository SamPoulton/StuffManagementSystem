using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Linq;

namespace StuffManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new NewItemWindow().ShowDialog();
            UpdateItems();
        }

        public void UpdateItems()
        {
            mainView.Items.Clear();
            foreach (Thing thing in DatabaseManager.Things)
                mainView.Items.Add(thing);
        }

        public void UpdateItems(string searchValue)
        {
            mainView.Items.Clear();
            foreach (Thing thing in DatabaseManager.Things.Where(i => i.Name.ToLower().Contains(searchValue.ToLower())))
                mainView.Items.Add(thing);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateItems();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new NewPlaceWindow().ShowDialog();
            UpdateItems();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            SearchBox.Foreground = Brushes.Black;
            SearchBox.FontStyle = FontStyles.Normal;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchBox.Text == "" || SearchBox.FontStyle == FontStyles.Italic) UpdateItems();
                else UpdateItems(SearchBox.Text);
            }
            catch (NullReferenceException) { };
        }
    }
}
