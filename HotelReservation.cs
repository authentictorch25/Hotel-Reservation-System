using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelResevationSystem
{
    public class HotelReservation
    {
        //Dictionary to store the details of hotel with name as the key
        public static Dictionary<string, Hotel> HotelBookRegular = new Dictionary<string, Hotel>();
        public static Dictionary<string, Hotel> HotelBookReward = new Dictionary<string, Hotel>();
        /// <summary>
        /// Adds the hotel.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="customer">The customer.</param>
        /// <param name="regularRate">The regular rate.</param>
        /// <param name="weekendRate">The weekend rate.</param>
        public static void AddHotel(string name, Customer customer, int regularRate, int weekendRate)
        {
            switch (customer)
            {
                case Customer.REGULAR_CUSTOMER:
                    //UC 3 Refactor
                    if (!HotelBookRegular.ContainsKey(name))
                    {
                        Hotel newHotel = new Hotel(name, regularRate, weekendRate);
                        HotelBookRegular.Add(name, newHotel);
                    }
                    else
                    {
                        Console.WriteLine($"Hotel {name} already exists in the records\n");
                    }
                    break;
                case Customer.REWARDS_CUSTOMER:
                    //UC 3 Refactor
                    if (!HotelBookReward.ContainsKey(name))
                    {
                        Hotel newHotel = new Hotel(name, regularRate, weekendRate);
                        HotelBookReward.Add(name, newHotel);
                    }
                    else
                    {
                        Console.WriteLine($"Hotel {name} already exists in the records\n");
                    }
                    break;
                default:
                    throw new HotelReservationCustomException(HotelReservationCustomException.ExceptionType.INVALID_CUSTOMER_TYPE, "INVALID CUSTOMER TYPE");
            }
        }
        public static void GetCheapestHotel()
        {
            var rateRecords = CalculateRateForEachHotel();
            //Finds the key-value pair where rate is minimum by sorting the dictionary values in ascending order
            var kvp = rateRecords.OrderBy(kvp => kvp.Value.totalRate).First();
            //Iterating the rateRecords dictionary to check how many hotels provide the minimum rate
            foreach (var v in rateRecords)
            {
                //Checks where the minimum rate matches and displays the HotelName and Rate
                if (v.Value.totalRate == kvp.Value.totalRate)
                    Console.WriteLine($"{v.Key},TotalRate:{v.Value.totalRate}");
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
                Console.WriteLine("Enter:\n1.If you are a REGULAR customer\n2.If you are a REWARDS customer");
                int options = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the check-in date(DDMMMYYYY):");
                DateTime checkIn = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter the check-out date(DDMMMYYYY):");
                DateTime checkOut = DateTime.Parse(Console.ReadLine());

                Dictionary<string, HotelRatingRate> rateRecordsForRegularCustomer = new Dictionary<string, HotelRatingRate>();
                Dictionary<string, HotelRatingRate> rateRecordsForRewardsCustomer = new Dictionary<string, HotelRatingRate>();

                if (options == 1)
                {
                    //UC 4 Refactor
                    foreach (var v in HotelBookRegular)
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
                        HotelRatingRate outputHotel = new HotelRatingRate(v.Key, v.Value.rating, totalRate);
                        rateRecordsForRegularCustomer.Add(v.Value.hotelName, outputHotel);
                    }
                    return rateRecordsForRegularCustomer;
                }
                else if (options == 2)
                {
                    //UC 4 Refactor
                    foreach (var v in HotelBookReward)
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
                        HotelRatingRate outputHotel = new HotelRatingRate(v.Key, v.Value.rating, totalRate);
                        rateRecordsForRewardsCustomer.Add(v.Value.hotelName, outputHotel);
                    }
                    return rateRecordsForRewardsCustomer;
                }
                else
                    throw new HotelReservationCustomException(HotelReservationCustomException.ExceptionType.INVALID_CUSTOMER_TYPE, "INVALID CUSTOMER TYPE");

            }
            catch (Exception e)
            {
                throw new HotelReservationCustomException(HotelReservationCustomException.ExceptionType.INVALID_DATE_FORMAT, "INVALID DATE FORMAT");
            }
        }

        /// <summary>
        /// Finds the cheapest best rated hotel.
        /// </summary>
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

        /// <summary>
        /// Adds the ratings and hotel.
        /// </summary>
        public static void AddRatingsAndHotel()
        {

            //Regular Customer rates
            AddHotel("Lakewood", Customer.REGULAR_CUSTOMER, 110, 90);
            AddHotel("Bridgewood", Customer.REGULAR_CUSTOMER, 150, 50);
            AddHotel("Ridgewood", Customer.REGULAR_CUSTOMER, 220, 150);

            //Rewards Customer Rates
            AddHotel("Lakewood", Customer.REWARDS_CUSTOMER, 80, 80);
            AddHotel("Bridgewood", Customer.REWARDS_CUSTOMER, 110, 50);
            AddHotel("Ridgewood", Customer.REWARDS_CUSTOMER, 100, 40);

            //UC 5
            AddRatings("Bridgewood", 4);
            AddRatings("Lakewood", 3);
            AddRatings("Ridgewood", 5);
        }
        /// <summary>
        /// Adds the ratings.
        /// </summary>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="rating">The rating.</param>
        public static void AddRatings(string hotelName, int rating)
        {
            foreach (var v in HotelBookRegular)
            {
                if (v.Key == hotelName)
                {
                    v.Value.rating = rating;
                    break;
                }
            }
        }
        /// <summary>
        /// Finds the best rated hotel.
        /// </summary>
        public static void FindBestRatedHotel()
        {
            var rateRecords = CalculateRateForEachHotel();
            //Finds the hotel with max rating
            int maxRating = rateRecords.Select(item => item.Value.rating).Max();
            foreach (var v in rateRecords)
            {
                //Check if hotels(one or many) have rating=maxRating and display their details
                if (v.Value.rating == maxRating)
                    Console.WriteLine($"{v.Key},TotalRate:{v.Value.totalRate}");
            }
        }

    }
}