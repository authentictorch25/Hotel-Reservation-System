using System;
using System.Collections.Generic;
using System.Text;

namespace HotelResevationSystem
{
    public class HotelReservation
    {
        //Dictionary to store the details of hotel with name as the key
        public static Dictionary<string, Hotel> HotelBook = new Dictionary<string, Hotel>();
        /// <summary>
        /// Adds the hotelto the dictionary.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="regularRate">The regular rate.</param>
        public static void AddHotel(string name, int regularRate)
        {
            if(HotelBook.ContainsKey(name))
            {
                Console.WriteLine("Hotel Name already exists");
            }
            else
            {
                //Adding hotel to the Hotel model
                Hotel hotel = new Hotel(name, regularRate);
                //Adding the hotel to dictionary
                HotelBook.Add(name, hotel);
            }
        }
    }
}
