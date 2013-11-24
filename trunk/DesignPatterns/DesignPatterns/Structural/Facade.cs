// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Structural
{
    class SkiRent
    {
        public int RentBoots(int feetSize, int skierLevel)
        {
            return 20;
        }
        public int RentSki(int weight, int skierLevel)
        {
            return 40;
        }
        public int RentPole(int height)
        {
            return 5;
        }
    }

    class SkiResortTicketSystem
    {
        public int BuyOneDayTicket()
        {
            return 120;
        }
        public int BuyHalfDayTicket()
        {
            return 60;
        }
    }

    class HotelBookingSystem
    {
        public int BookRoom(int roomQuality)
        {
            switch (roomQuality)
            {
                case 3:
                    return 250;
                case 4:
                    return 500;
                case 5:
                    return 900;
                default:
                    throw new ArgumentException("roomQuality should be in range [3;5]");
            }
        }
    }

    class SkiResortFacade
    {
        private SkiRent _skiRent = new SkiRent();
        private SkiResortTicketSystem _skiResortTicketSystem = new SkiResortTicketSystem();
        private HotelBookingSystem _hotelBookingSystem = new HotelBookingSystem();

        public int HaveGoodRest(int height, int weight, int feetSize, int skierLevel, int roomQuality)
        {
            int skiPrice = _skiRent.RentSki(weight, skierLevel);
            int skiBootsPrice = _skiRent.RentBoots(feetSize, skierLevel);
            int polePrice = _skiRent.RentPole(height);
            int oneDayTicketPrice = _skiResortTicketSystem.BuyOneDayTicket();
            int hotelPrice = _hotelBookingSystem.BookRoom(roomQuality);

            return skiPrice + skiBootsPrice + polePrice + oneDayTicketPrice + hotelPrice;
        }

        public int HaveRestWithOwnSkis()
        {
            int oneDayTicketPrice = _skiResortTicketSystem.BuyOneDayTicket();
            return oneDayTicketPrice;
        }
    }

    public class FacadeDemo
    {
        public static void Run()
        {
            var skiResortFacade = new SkiResortFacade();
            int weekendRestPrice = skiResortFacade.HaveGoodRest(175, 60, 42, 2, 3);
            Console.WriteLine("Price: {0}", weekendRestPrice);
        }
    }
}