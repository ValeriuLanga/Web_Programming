using Lab_9.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Lab_9.DataAbstractionLayer
{
    public class DAL
    {
        public User Authenticate(string userName, string password)
        {
            User user = null;

            try
            {
                MySqlCommand getUserCommand = new MySqlCommand();
                getUserCommand.Connection   = InitializeConnection();
                getUserCommand.CommandText  = "SELECT * FROM users WHERE Name = '" + userName + "' AND Password = '" + password + "'";

                MySqlDataReader reader = getUserCommand.ExecuteReader();

                // i.e. we have a match in the db
                if(reader.Read())
                {
                    user = new User();
                    user.UserId = reader.GetInt32("UserId");
                    user.Name = userName;
                    user.Password = password;
                }

                reader.Close();
            }
            catch(MySqlException exception)
            {
                Debug.WriteLine(exception.Message);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unhandled exception caught!");
            }

            return user;

        }
        public List<Room> GetAllRooms()
        {
            List<Room> roomsList = new List<Room>();

            try
            {
                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.Connection = InitializeConnection();

                mysqlCommand.CommandText = "select * from rooms";

                MySqlDataReader dataReader = mysqlCommand.ExecuteReader();
                roomsList = ParseReaderResults(dataReader);
                dataReader.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                Debug.WriteLine(exception.Message);
            }
            catch(Exception)
            {
                Debug.WriteLine("Unhandled Exception Caught!");
            }
            
            return roomsList;
        }

        public List<Room> GetRoomsFromPrice(int price)
        {
            List<Room> roomsList = new List<Room>();

            try
            {
                MySqlCommand mysqlCommand = new MySqlCommand();
                mysqlCommand.Connection = InitializeConnection();

                mysqlCommand.CommandText = "select * from rooms where Price <= " + price;

                MySqlDataReader dataReader = mysqlCommand.ExecuteReader();
                roomsList = ParseReaderResults(dataReader);
                dataReader.Close();

            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                Debug.WriteLine("\t[ERROR] " + exception.Message);
            }
            catch (Exception)
            {
                Debug.WriteLine("\t[ERROR] Unhandled Exception Caught!");
            }

            return roomsList;
        }

        // returns  true if the booking was a success
        //          false otherwise
        public bool BookRoom(int roomId, string guestName, DateTime beginDate, DateTime endDate)
        {
            bool found = false;

            try
            {
                // get the room with the given id
                List<Room> roomsList = GetAllRooms();

                foreach (Room room in roomsList)
                {
                    // if the guest name would not be "" or the current guest name
                    // we should not update the room
                    if (room.RoomId == roomId && (room.GuestName == "" || room.GuestName == guestName))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    UpdateRoomWithBookingInfo(guestName, roomId, beginDate, endDate);
                }
                else
                {
                    Debug.WriteLine("\t[ERROR] Could not match Room Id with Guest Name!");
                }
            }
            catch(MySqlException mysqlException)
            {
                Debug.WriteLine("\t[ERROR] " + mysqlException.Message);
            }

            return found;
        }

        private void UpdateRoomWithBookingInfo(string guestName, int roomId, DateTime beginDate, DateTime endDate)
        {
            MySqlCommand mysqlCommand = new MySqlCommand();

            mysqlCommand.Connection = InitializeConnection();

            mysqlCommand.CommandText =
                "UPDATE rooms SET " +
                "GuestName = '" + guestName + "', " +
                "BeginDate = '" + beginDate.ToString("yyyy-MM-dd") + "', " +
                "EndDate = '" + endDate.ToString("yyyy-MM-dd") + "' " +
                "WHERE RoomId = " + roomId.ToString();
            Debug.WriteLine(mysqlCommand.CommandText);

            int affectedRows = mysqlCommand.ExecuteNonQuery();
            Debug.WriteLine("[Info] Update Query affected {0} rows", affectedRows);

            mysqlCommand.Connection.Close();
        }

        private List<Room> ParseReaderResults(MySqlDataReader mysqlDataReader)
        {
            List<Room> roomsList = new List<Room>();

            while (mysqlDataReader.Read())
            {
                Room room = new Room();
                room.RoomId = mysqlDataReader.GetInt32("RoomId");
                room.Category = mysqlDataReader.GetString("Category");
                room.Price = mysqlDataReader.GetInt32("Price");
                room.HotelName = mysqlDataReader.GetString("HotelName");
                room.GuestName = mysqlDataReader.GetString("GuestName");
                room.BeginDate = mysqlDataReader.GetDateTime("BeginDate");
                room.EndDate = mysqlDataReader.GetDateTime("EndDate");

                roomsList.Add(room);
            }

            return roomsList;
        }

        private MySqlConnection InitializeConnection()
        {
            MySql.Data.MySqlClient.MySqlConnection connection;
            connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = m_connectionString;

            connection.Open();

            return connection;
        }

        private string m_connectionString = "server=localhost; uid=vlanga; pwd=VpnZmrWzP49oef6j; database=lab_7; SslMode=None; Convert Zero Datetime=True";
        
    }
}