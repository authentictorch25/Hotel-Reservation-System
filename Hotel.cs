using System;
using System.Collections.Generic;
using System.Text;

namespace HotelResevationSystem
{
    public class Hotel
    {
        //Intialising name and rate for a hotel
        public string hotelName;
        public int regularRate;
        /// <summary>
        /// Initializes a new instance of the <see cref="Hotel"/> class.
        /// </summary>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="regularRate">The regular rate.</param>
        public Hotel(string hotelName, int regularRate )
        {
            this.hotelName = hotelName;
            this.regularRate = regularRate;
        }
    }
}
