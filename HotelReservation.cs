using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void AddHotel(string name, int regularRate, int weekendRate)
        {
            if(HotelBook.ContainsKey(name))
            {
                Console.WriteLine("Hotel Name already exists");
            }
            else
            {
                //Adding hotel to the Hotel model
                Hotel hotel = new Hotel(name, regularRate, weekendRate);
                //Adding the hotel to dictionary
                HotelBook.Add(name, hotel);
            }
        }
        public static void GetCheapestHotel()
        {
            try
            {
                //Entering the checkin date
                Console.WriteLine("Enter the checkin date, Format DDMMYYYY");
                //Entering the checkout date
                DateTime checkIn = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter the checkout date, Format DDMMYYYY");
                DateTime checkOut = DateTime.Parse(Console.ReadLine());
                //Count of days stayed (suppose a person stays from 2/10-3/10, Substraction will give 1 but actual stay is 2 days hence +1)
                int numOfDays = (checkOut - checkIn).Days + 1;
                Dictionary<string, int> rateRecords = new Dictionary<string, int>();
                //Adding a dictionary to get the rates and name of the hotel
                foreach (var v in HotelBook)
                {
                    int totalRate = 0;
                    DateTime tempDate = checkIn;
                    while (tempDate <= checkOut)
                    {
                        //Checking if the day is weekend
                        if (tempDate.DayOfWeek == DayOfWeek.Saturday || tempDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            totalRate += v.Value.weekendRate;
                        }
                        else
                        {
                            totalRate += v.Value.regularRate;
                        }
                        //Incrementing the current tempdate to next day
                        tempDate = tempDate.AddDays(1);
                    }
                    rateRecords.Add(v.Value.hotelName, totalRate);
                }
                //Finds the key-value pair where rate is minimum by sorting the dictionary values in ascending order
                var hotelDetails = rateRecords.OrderBy(kvp => kvp.Value).First();
                //Iterating the rateRecords dictionary to check how many hotels provide the minimum rate
                foreach (var v in rateRecords)
                {
                    if (v.Value == hotelDetails.Value)
                        Console.WriteLine($"{v.Key},TotalRate:{v.Value}");
                }
            }
            //In case any exception Arises
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
