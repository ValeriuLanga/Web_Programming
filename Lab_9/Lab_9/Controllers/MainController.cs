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
                Console.Write("User login failed!");
                return View("Error");
            }

            // set up the ViewData with our user, so he can book rooms
            ViewData["user"] = user;
            return View("FilterRooms");
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
                Console.Write("Could not convert price to int!");
                return "";
            }

            DAL dataAbstractionLayer = new DAL();
            List<Room> roomsList = dataAbstractionLayer.GetRoomsFromPrice(price);
            ViewData["roomsList"] = roomsList;

            string result = m_tableHeader;

            foreach ( Room room in roomsList)
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

            DateTime beginDate, endDate;

            if(!DateTime.TryParse(Request.Params["beginDate"], out beginDate))
            {
                Console.Write("Failed to parse begin date from parameters!");
            }

            if (!DateTime.TryParse(Request.Params["endDate"], out endDate))
            {
                Console.Write("Failed to parse begin date from parameters!");
            }


            Console.Write("Bookin room for client {1}, begin date {2}, end date {3}", userName, beginDate, endDate);

            return "";
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
            tableData += "<td>" + room.BeginDate.ToString("dd-mm-yyyy") + "</td>";
            tableData += "<td>" + room.EndDate.ToString("dd-mm-yyyy") + "</td>";

            return tableData;
        }

        private string m_tableHeader = 
            "<table> <thead> <th>RoomId</th> <th>Category</th> <th>Price</th> <th>Hotel Name</th> <th>Guest Name</th> <th>Begin Date</th> <th>End Date</th> </thead>";
    }
}