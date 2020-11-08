using System;

namespace HotelResevationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //UC1
            Console.WriteLine("Welcome to Hotel Reservation Sysyem");
            HotelReservation.AddHotel("Lakewood", 110);
            HotelReservation.AddHotel("Bridgewood", 150);
            HotelReservation.AddHotel("Ridgewood", 220);
            
            //UC2
            HotelReservation.GetCheapestHotel();
            Console.ReadKey();
        }
    }
}
