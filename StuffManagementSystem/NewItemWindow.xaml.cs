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
    public partial class NewItemWindow : Window
    {
        public NewItemWindow()
        {
            InitializeComponent();
            cboGroups.ItemsSource = DatabaseManager.Places.Select(i => i.Name);
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.AddThing(new Thing
            {
                Name = boxName.Text,
                Place = cboGroups.Text,
                Description = boxDesc.Text,
                Count = int.Parse(boxCount.Text),
            });
            Close();
        }
    }
}
