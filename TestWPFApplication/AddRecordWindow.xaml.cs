using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TestWPFApplication
{
    /// <summary>
    /// Interaction logic for AddRecordWindow.xaml
    /// </summary>
    public partial class AddRecordWindow : Window
    {
        public readonly MainWindow main;
        public readonly CustomerDBEntities customerDB;
        public AddRecordWindow(CustomerDBEntities db,MainWindow mainWindow)
        {
            customerDB = db;
            main = mainWindow;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Customer customerObject = new Customer()
            {
                Name = NameTextBox.Text,
                Age = int.Parse(AgeTextBox.Text),
                Post_Code = PostCodeTextBox.Text,
                Height = decimal.Parse(HeightTextBox.Text)
            };

            customerDB.Customers.Add(customerObject);
            customerDB.SaveChanges();
            main.UpdateDataGrid();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
