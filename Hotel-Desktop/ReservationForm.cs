using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Hotel_Desktop
{
    public partial class ReservationForm : Form
    {
        public ReservationForm()
        {
            InitializeComponent();

            /*
            dataGridView1.Columns.Add("ReservationId", "Reservation ID");
            dataGridView1.Columns.Add("RoomNumber", "Room Number");
            dataGridView1.Columns.Add("CheckInDate", "Check In Date");
            dataGridView1.Columns.Add("CheckOutDate", "Check Out Date");
            dataGridView1.Columns.Add("CustomerName", "Customer Name");
            dataGridView1.Columns.Add("Requests", "Requests");
            */

            LoadReservations();

            
        }

        private void LoadReservations()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            string query = "SELECT * FROM Booking";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable BookingTable = new DataTable();
                    adapter.Fill(BookingTable);
                    dataGridView1.DataSource = BookingTable;
                }
            }
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))

            if (selectedItem == "Room Service")
            {
                connection.Open();
                //dataGridView1.CurrentRow.Cells["Requests"].Value = "Room Service";
                SqlCommand cmd = new SqlCommand("UPDATE Booking SET RequestType='Room Service', RequestStatus='New' WHERE BookingId=@BookingId", connection);
                cmd.Parameters.AddWithValue("@BookingId", dataGridView1.CurrentRow.Cells["BookingId"].Value);
                cmd.ExecuteNonQuery();
                connection.Close();

                    LoadReservations();
            }
            else if (selectedItem == "Maintenance Request")
            {
                    connection.Open();
                //dataGridView1.CurrentRow.Cells["Requests"].Value = "Maintenance";
                SqlCommand cmd = new SqlCommand("UPDATE Booking SET RequestType='Maintenance', RequestStatus='New' WHERE BookingId=@BookingId", connection);
                cmd.Parameters.AddWithValue("@BookingId", dataGridView1.CurrentRow.Cells["BookingId"].Value);
                cmd.ExecuteNonQuery();
                    connection.Close();
                    LoadReservations();
                }
            else if (selectedItem == "Cleaning")
            {
                    connection.Open();
                //dataGridView1.CurrentRow.Cells["Requests"].Value = "Cleaning";
                SqlCommand cmd = new SqlCommand("UPDATE Booking SET RequestType='Cleaning', RequestStatus='New' WHERE BookingId=@BookingId", connection);
                cmd.Parameters.AddWithValue("@BookingId", dataGridView1.CurrentRow.Cells["BookingId"].Value);
                cmd.ExecuteNonQuery();
                    connection.Close();
                    LoadReservations();
            }
                else if (selectedItem == "No Requests")
                {
                    connection.Open();
                    //dataGridView1.CurrentRow.Cells["Requests"].Value = "No Requests";
                    SqlCommand cmd = new SqlCommand("UPDATE Booking SET RequestType='No Requests', RequestStatus='Finished' WHERE BookingId=@BookingId", connection);
                    cmd.Parameters.AddWithValue("@BookingId", dataGridView1.CurrentRow.Cells["BookingId"].Value);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    LoadReservations();
                }
        }

        private void btnCheckIn_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int BookingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BookingId"].Value);

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                string query = "UPDATE Booking SET Status = @Status WHERE BookingId = @BookingId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", "Checked In");
                        command.Parameters.AddWithValue("@BookingId", BookingId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            dataGridView1.SelectedRows[0].Cells["Status"].Value = "Checked In";
                            MessageBox.Show("Reservation checked in successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Error checking in reservation.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to check in.");
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int BookingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BookingId"].Value);

                string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                string query = "UPDATE Booking SET Status = @Status WHERE BookingId = @BookingId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", "Checked Out");
                        command.Parameters.AddWithValue("@BookingId", BookingId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            dataGridView1.SelectedRows[0].Cells["Status"].Value = "Checked Out";
                            MessageBox.Show("Reservation checked out successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Error checking out reservation.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to check in.");
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            LoadReservations();
        }
    }
}
