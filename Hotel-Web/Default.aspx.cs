using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    resultLabel.Text = "Connection succesful!";
                }
                catch (Exception ex)
                {
                    resultLabel.Text = "Connection failed: " + ex.Message;
                }

                SqlCommand command = new SqlCommand("Select * FROM Rooms", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int roomId = reader.GetInt32(0);
                        string roomName = reader.GetString(1);
                        int numBeds = reader.GetInt32(2);
                        int roomSize = reader.GetInt32(3);
                        string roomType = reader.GetString(4);

                        Response.Write("Room ID: " + roomId + "<br>");
                        Response.Write("Room Name: " + roomName + "<br>");
                        Response.Write("Number of Beds: " + numBeds + "<br>");
                        Response.Write("Room Size: " + roomSize + "<br>");
                        Response.Write("Room Type: " + roomType + "<br><br>");
                    }
                }
            }

        }

        protected void btnBookRoom_Click(object sender, EventArgs e)
        {
            string roomType = "Single";
            DateTime checkInDate = DateTime.Now;
            DateTime checkOutDate = DateTime.Now.AddDays(1);
            string customerName = "Viggo wideroe";
            string customerEmail = "viggo@wideroe.com";
            string customerPhone = "75535010";

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string insertSql = "INSERT INTO Booking (RoomType, CheckInDate, CheckOutDate, CustomerName, CustomerEmail, CustomerPhone, Status, RequestType) " +
                                "VALUES (@RoomType, @CheckInDate, @CheckOutDate, @CustomerName, @CustomerEmail, @CustomerPhone, @Status, @RequestType)";
            SqlCommand command = new SqlCommand(insertSql, connection);

            command.Parameters.AddWithValue("@RoomType", roomType);
            command.Parameters.AddWithValue("@CheckInDate", checkInDate);
            command.Parameters.AddWithValue("@CheckOutDate", checkOutDate);
            command.Parameters.AddWithValue("@CustomerName", customerName);
            command.Parameters.AddWithValue("@CustomerEmail", customerEmail);
            command.Parameters.AddWithValue("@CustomerPhone", customerPhone);
            command.Parameters.AddWithValue("@Status", "Booked");
            command.Parameters.AddWithValue("@RequestType", "None");

            command.ExecuteNonQuery();

            connection.Close();

            Response.Redirect("BookingConfirmation.aspx");
        }
    }
}