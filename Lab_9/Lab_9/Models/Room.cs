using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_9.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string HotelName { get; set; }
        public string GuestName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}