using System;
using System.Collections.Generic;
using System.Text;

namespace HotelResevationSystem
{
    public class HotelRatingRate
    {
        public string hotelName;
        public int rating;
        public int totalRate;

        public HotelRatingRate(string hotelName, int rating, int totalRate)
        {
            this.hotelName = hotelName;
            this.rating = rating;
            this.totalRate = totalRate;

        }
    }
}
