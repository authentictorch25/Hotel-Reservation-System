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
            if (HotelBook.ContainsKey(name))
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
        /// <summary>
        /// Adds the ratings.
        /// </summary>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="ratings">The ratings.</param>
        public static void AddRatings(string hotelName, int ratings)
        {
            {
                //Iterating in the dictionary to get name and add rating
                foreach (var v in HotelBook)
                {
                    if (v.Key == hotelName)
                    {
                        v.Value.rating = ratings;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Calculates the rate for each hotel.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Dictionary<string, HotelRatingRate> CalculateRateForEachHotel()
        {
            try
            {
                Console.WriteLine("Enter the check-in date(DDMMMYYYY):");
                DateTime checkIn = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter the check-out date(DDMMMYYYY):");
                DateTime checkOut = DateTime.Parse(Console.ReadLine());
                Dictionary<string, HotelRatingRate> rateRecords = new Dictionary<string, HotelRatingRate>();

                //UC 4 Refactor
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
                    //UC 6 Refactor
                    HotelRatingRate HotelRatingRate = new HotelRatingRate(v.Key, v.Value.rating, totalRate);
                    rateRecords.Add(v.Value.hotelName, HotelRatingRate);
                }
                return rateRecords;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void FindCheapestBestRatedHotel()
        {
            //Get the raterecords dictionary
            var rateRecords = CalculateRateForEachHotel();
            //Dictionary initialized to store details of hotels with cheapest rate
            Dictionary<string, HotelRatingRate> cheapestRateDict = new Dictionary<string, HotelRatingRate>();
            var kvp = rateRecords.OrderBy(kvp => kvp.Value.totalRate).First();
            foreach (var v in rateRecords)
            {
                if (v.Value.totalRate == kvp.Value.totalRate)
                    //Add all hotels with same minimum totalRate
                    cheapestRateDict.Add(v.Key, v.Value);
            }
            //Calculates the max rating among all hotels with same totalRate
            var maxRating = cheapestRateDict.Select(item => item.Value.rating).Max();
            foreach (var v in cheapestRateDict)
            {
                //Checks how many hotels have the rating=maxRating and prints the details
                if (v.Value.rating == maxRating)
                    Console.WriteLine($"{v.Key},Ratings:{v.Value.rating},TotalRate:{v.Value.totalRate}");
            }
        }
    }
}
