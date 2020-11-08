using System;

namespace HotelResevationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //UC1
            Console.WriteLine("Welcome to Hotel Reservation Sysyem");
            HotelReservation.AddHotel("Lakewood", 110,90);
            HotelReservation.AddHotel("Bridgewood", 150,60);
            HotelReservation.AddHotel("Ridgewood", 220,150);
            
            //UC2,//UC3,//UC4
            HotelReservation.GetCheapestHotel();

            //UC5
            HotelReservation.AddRatings("Lakewood",3);
            HotelReservation.AddRatings("Bridgewood",4);
            HotelReservation.AddRatings("Ridgewood",5);
            

            //UC6
            HotelReservation.FindCheapestBestRatedHotel();
            Console.ReadKey();

            //UC7
            HotelReservation.FindBestRatedHotel();
        }
    }
}
