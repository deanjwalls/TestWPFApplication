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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly CustomerDBEntities customerDB;

        public Customer CurrentSelectedCustomer = null;
        
        public MainWindow()
        {
            InitializeComponent();
            customerDB = new CustomerDBEntities();

            this.CustomerDataGrid.ItemsSource = customerDB.Customers.ToList();
        }

        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecordWindow addRecord = new AddRecordWindow(customerDB,this,null);
            addRecord.ShowDialog();
        }
        private void EditRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSelectedCustomer != null)
            {
                AddRecordWindow editRecord = new AddRecordWindow(customerDB,this, CurrentSelectedCustomer);
                editRecord.ShowDialog();
            }
            else
            {
                Debug.WriteLine("Can't edit a customer if no customer is selected");
            }
        }

        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem != null)
            {
                if (CustomerDataGrid.SelectedItem.GetType() == typeof(Customer))
                {
                    EditButton.IsEnabled = true;
                    CurrentSelectedCustomer = (Customer)CustomerDataGrid.SelectedItem;
                }
                else
                {
                    EditButton.IsEnabled = false;
                }
            }
            else
            {
                EditButton.IsEnabled = false;
            }
        }        
        public void UpdateDataGrid()
        {
            this.CustomerDataGrid.ItemsSource = customerDB.Customers.ToList();
        }
    }
}
