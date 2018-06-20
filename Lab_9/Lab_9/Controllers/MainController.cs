/*
 Write a web application for room booking in a hotel chain. The application should save
room information in the database. The clients should have the posibility of 

- browsing the rooms by category, type, price, hotel etc. (use AJAX for this), 
- booking one or more rooms for a specific period of time, but also they should have the 
- posibility of cancelling their reservation. 

Rooms browsing should be paged - rooms are displayed on pages with maximum
4 rooms on a page (you should be able to go to the previous and the next page).

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab_9.Models;
using Lab_9.DataAbstractionLayer;
using System.Diagnostics;

namespace Lab_9.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Login()
        {
            DAL dal = new DataAbstractionLayer.DAL();

            string userName = Request.Params["username"];
            string password = Request.Params["password"];

            // see if we have this user
            User user = dal.Authenticate(userName, password);
            if(user == null)
            {
                Debug.WriteLine("User login failed!");
                return View("Error");
            }

            // set up the ViewData with our user, so he can book rooms
            ViewData["user"] = user;
            return View("FilterRooms");
        }

        

        public string GetRoomCategoriesAsHtmlFilter()
        {
            DAL dal = new DAL();
            List<Room> roomsList = dal.GetAllRooms();

            string htmlFilter = "<select id='select_types' name='types'>Filter by room category:</option>";

            HashSet<string> roomCategories = new HashSet<string>();

            foreach (Room room in roomsList)
            {
                roomCategories.Add(room.Category);
            }

            foreach(string category in roomCategories)
            {
                htmlFilter += "<option value='" + category + "'>" + category + "</option>";

            }

            return htmlFilter;
        }

        public string GetAllRooms()
        {
            DAL dataAbstractionLayer = new DAL();

            List<Room> roomsList = dataAbstractionLayer.GetAllRooms();

            ViewData["roomsList"] = roomsList;

            string result = m_tableHeader;

            foreach (Room room in roomsList)
            {
                result += AddRoomFieldsToTableData(room);
            }

            result += "</table>";

            return result;
        }

        public string GetRoomsWithGivenPrice()
        {
            int price;
            if (!int.TryParse(Request.Params["price"], out price))
            {
                Debug.WriteLine("Could not convert price to int!");
                return "";
            }

            DAL dataAbstractionLayer = new DAL();
            List<Room> roomsList = dataAbstractionLayer.GetRoomsFromPrice(price);
            ViewData["roomsList"] = roomsList;

            // form the html table
            string result = m_tableHeader;
            foreach ( Room room in roomsList)
            {
                result += AddRoomFieldsToTableData(room);
            }

            result += "</table>";
            return result; 
        }

        public string GetRoomsByCategoryHtml()
        {
            DAL dal = new DataAbstractionLayer.DAL();

            // get the category from the request
            string category = Request.Params["category"];

            List<Room> roomsList = dal.GetAllRooms();
            List<Room> matchingRooms = new List<Room>();

            foreach (Room room in roomsList)
            {
                if (room.Category == category)
                    matchingRooms.Add(room);
            }

            string result = m_tableHeader;
            foreach (Room room in matchingRooms)
            {
                result += AddRoomFieldsToTableData(room);
            }
            result += "</table>";

            return result;
        }

        public string BookRoom()
        {
            // get our params, i.e. UserName, BeginDate, EndDate
            string userName = Request.Params["userName"];
            DAL dal = new DAL();

            // parse the room id
            int roomId;
            if(!int.TryParse(Request.Params["roomId"], out roomId))
            {
                Debug.WriteLine("\t[ERROR] Failed to parse room id from parameters!");
                return "";
            }

            // parse the dates
            DateTime beginDate, endDate;

            if (!DateTime.TryParse(Request.Params["beginDate"], out beginDate))
            {
                Debug.WriteLine("\t[ERROR] Failed to parse begin date from parameters!");
                return "";
            }

            if (!DateTime.TryParse(Request.Params["endDate"], out endDate))
            {
                Debug.WriteLine("\t[ERROR] Failed to parse begin date from parameters!");
                return "";
            }

            Debug.WriteLine("[INFO] Booking room id = {0} for client name = {1}, begin date = {2}, end date = {3}", 
                roomId, 
                userName, 
                beginDate.ToString("yyyy-MM-dd"), 
                endDate.ToString("yyyy-MM-dd"));

            // update the room with the given data
            bool updated = dal.BookRoom(roomId, userName, beginDate, endDate);
            if(!updated)
            {
                Debug.WriteLine("[ERROR] BookRoom Failed!");
                return "";
            }
            
            // update the new rooms list
            return GetAllRooms();
        }

        public string DeleteReservation()
        {
            // get our params, i.e. UserName, BeginDate, EndDate
            string guestName = Request.Params["userName"];
            DAL dal = new DAL();

            // parse the room id
            int roomId;
            if (!int.TryParse(Request.Params["roomId"], out roomId))
            {
                Debug.WriteLine("\t[ERROR] Failed to parse room id from parameters!");
                return "false";
            }

            // return some ugly looking string boolean
            return (dal.DeleteReservation(guestName, roomId)) ? "true" : "false" ;
        }

        // GET: Main
        public ActionResult Index()
        {
            return View("Login");
        }

        private string AddRoomFieldsToTableData(Room room)
        {
            string tableData = "";
            tableData += "<tr>";
            tableData += "<td>" + room.RoomId + "</td>";
            tableData += "<td>" + room.Category + "</td>";
            tableData += "<td>" + room.Price + "</td>";
            tableData += "<td>" + room.HotelName + "</td>";
            tableData += "<td>" + room.GuestName + "</td>";
            tableData += "<td>" + room.BeginDate.ToString("dd-MM-yyyy") + "</td>";
            tableData += "<td>" + room.EndDate.ToString("dd-MM-yyyy") + "</td>";

            return tableData;
        }

        private string m_tableHeader = 
            "<table> <thead> <th>RoomId</th> <th>Category</th> <th>Price</th> <th>Hotel Name</th> <th>Guest Name</th> <th>Begin Date</th> <th>End Date</th> </thead>";
    }
}