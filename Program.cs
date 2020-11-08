using System;

namespace HotelResevationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //Addition of hotel with ratings into records
            HotelReservation.AddRatingsAndHotel();

            //UC 2
            HotelReservation.GetCheapestHotel();

            //UC 6
            HotelReservation.FindCheapestBestRatedHotel();

            //UC 7
            HotelReservation.FindBestRatedHotel();
            Console.ReadKey();
        }
    }
}
