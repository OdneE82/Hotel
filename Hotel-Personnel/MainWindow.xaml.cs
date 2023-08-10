using Hotel_Personnel.HotelDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;

namespace Hotel_Personnel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HotelDataSet dataSet = new HotelDataSet();
            BookingTableAdapter bookingAdapter = new BookingTableAdapter();
            bookingAdapter.Fill(dataSet.Booking);
            bookingGrid.ItemsSource = dataSet.Booking.DefaultView;

        }

        private void RefreshGrid()
        {
            HotelDataSet dataSet = new HotelDataSet();
            BookingTableAdapter bookingAdapter = new BookingTableAdapter();
            bookingAdapter.Fill(dataSet.Booking);
            bookingGrid.ItemsSource = dataSet.Booking.DefaultView;
        }




        private void finish_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)bookingGrid.SelectedItem;
            if (selectedRow != null)
            {
                int bookingId = (int)selectedRow["BookingId"];
                string connectedString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectedString))
                {
                    connection.Open();
                    string updateSql = "UPDATE Booking SET RequestStatus = 'Finished' WHERE BookingId = @BookingId";
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@BookingId", bookingId);
                    command.ExecuteNonQuery();
                }
                RefreshGrid();
            }
        }


        private void progress_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)bookingGrid.SelectedItem;
            if (selectedRow != null)
            {
                int bookingId = (int)selectedRow["BookingId"];
                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateSql = "UPDATE Booking SET RequestStatus = 'In Progress' WHERE BookingId = @BookingId";
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@BookingId", bookingId);
                    command.ExecuteNonQuery();
                }
                RefreshGrid();
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }
    }
}
