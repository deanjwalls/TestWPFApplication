using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Customer customer;

        public AddRecordWindow(CustomerDBEntities db,MainWindow mainWindow, Customer customerToEdit)
        {
            customerDB = db;
            main = mainWindow;
            InitializeComponent();
            if (customerToEdit != null)
            {
                SubmitButton.Content = "Update";
                this.customer = customerToEdit;
                NameTextBox.Text = customer.Name;
                AgeTextBox.Text = customer.Age.ToString();
                PostCodeTextBox.Text = customer.Post_Code;
                HeightTextBox.Text = customer.Height.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            { 
                if (customer == null)
                {
                    Customer customerObject = new Customer()
                    {
                        Name = NameTextBox.Text,
                        Age = int.Parse(AgeTextBox.Text),
                        Post_Code = PostCodeTextBox.Text,
                        Height = decimal.Parse(HeightTextBox.Text)
                    };

                    customerDB.Customers.AddOrUpdate(customerObject);
                }
                else
                {
                    customer.Name = NameTextBox.Text;
                    customer.Age = int.Parse(AgeTextBox.Text);
                    customer.Post_Code = PostCodeTextBox.Text;
                    customer.Height = decimal.Parse(HeightTextBox.Text);

                    customerDB.Customers.AddOrUpdate(customer);
                }

                customerDB.SaveChanges();
                main.UpdateDataGrid();
                this.Close();
            }
            ErrorText.Text = "One or more fields are Invalid";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool Validation()
        {
            bool CheckName = false;
            bool CheckAge = false;
            bool CheckPostCode = false;
            bool CheckHeight = false;

            if(!String.IsNullOrEmpty(NameTextBox.Text))
            {
                CheckName = NameTextBox.Text.Length <= 50;
            }

            if (!String.IsNullOrEmpty(AgeTextBox.Text))
            {
                int IntToCheck = int.TryParse(AgeTextBox.Text, out _) ? int.Parse(AgeTextBox.Text) : - 1;
                CheckAge = IntToCheck >= 0 && IntToCheck <= 110;
            }

            if (!String.IsNullOrEmpty(PostCodeTextBox.Text))
            {
                CheckPostCode = PostCodeTextBox.Text.Any(char.IsLetter) && PostCodeTextBox.Text.Any(char.IsDigit);
            }

            if (!String.IsNullOrEmpty(HeightTextBox.Text))
            {
                decimal decimalToCheck = decimal.TryParse(HeightTextBox.Text, out _) ? decimal.Parse(HeightTextBox.Text) : (decimal) 1.000;
                CheckHeight = Decimal.Round(decimalToCheck,2) == decimalToCheck;
            }

            if (CheckName && CheckAge && CheckPostCode && CheckHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
