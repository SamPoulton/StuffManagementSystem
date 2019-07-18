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
using System.Windows.Shapes;

namespace StuffManagementSystem
{
    /// <summary>
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
    public partial class NewPlaceWindow : Window
    {
        public NewPlaceWindow()
        {
            InitializeComponent();
            cboGroups.ItemsSource = DatabaseManager.Groups.Select(i => i.Name);
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.AddPlace(new Place
            {
                Name = boxName.Text,
                Group = cboGroups.Text,
                Description = boxDesc.Text,
            });
            Close();
        }
    }
}
