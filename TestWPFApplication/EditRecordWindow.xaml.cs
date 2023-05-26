using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for EditRecordWindow.xaml
    /// </summary>
    public partial class EditRecordWindow : Window
    {
        public Customer customer;
        public MainWindow main;
        public readonly CustomerDBEntities customerDB;
        public EditRecordWindow(CustomerDBEntities db,MainWindow mainWindow, Customer customerToEdit)
        {
            customerDB = db;
            this.main = mainWindow;
            InitializeComponent();
            this.customer = customerToEdit;
            NameTextBox.Text = customer.Name;
            AgeTextBox.Text = customer.Age.ToString();
            PostCodeTextBox.Text = customer.Post_Code;
            HeightTextBox.Text = customer.Height.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            customer.Name = NameTextBox.Text;
            customer.Age = int.Parse(AgeTextBox.Text);
            customer.Post_Code = PostCodeTextBox.Text;
            customer.Height = decimal.Parse(HeightTextBox.Text);

            customerDB.Customers.AddOrUpdate(customer);
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
