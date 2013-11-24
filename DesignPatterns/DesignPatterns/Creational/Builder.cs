// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Creational
{
    /* Abstract Builder */
    public abstract class LaptopBuilder
    {
        protected Laptop Laptop { get; private set; }
        public void CreateNewLaptop()
        {
            Laptop = new Laptop();
        }
        public Laptop GetMyLaptop()
        {
            return Laptop;
        }
        //mentioned steps to build laptop
        public abstract void SetMonitorResolution();
        public abstract void SetProcessor();
        public abstract void SetMemory();
        public abstract void SetHDD();
        public abstract void SetBattery();
    }

    /* Concrete Builder */
    public class TripLaptopBuilder : LaptopBuilder
    {
        public override void SetMonitorResolution()
        {
            Laptop.MonitorResolution = "1200X800";
        }
        public override void SetProcessor()
        {
            Laptop.Processor = "Celeron 2 GHz";
        }
        public override void SetMemory()
        {
            Laptop.Memory = "2048 Mb";
        }
        public override void SetHDD()
        {
            Laptop.HDD = "250 Gb";
        }
        public override void SetBattery()
        {
            Laptop.Battery = "12 lbs";
        }
    }

    /* Concrete Builder */
    public class GamingLaptopBuilder : LaptopBuilder
    {
        public override void SetMonitorResolution()
        {
            Laptop.MonitorResolution = "1900X1200";
        }
        public override void SetProcessor()
        {
            Laptop.Processor = "Core 2 Duo, 3.2 GHz";
        }
        public override void SetMemory()
        {
            Laptop.Memory = "6144 Mb";
        }
        public override void SetHDD()
        {
            Laptop.HDD = "500 Gb";
        }
        public override void SetBattery()
        {
            Laptop.Battery = "6 lbs";
        }
    }

    /* Director */
    public class BuyLaptop
    {
        private LaptopBuilder _laptopBuilder;
        public void SetLaptopBuilder(LaptopBuilder lBuilder)
        {
            _laptopBuilder = lBuilder;
        }
        // Змушує будівельника повернути цілий ноутбук
        public Laptop GetLaptop()
        {
            return _laptopBuilder.GetMyLaptop();
        }
        // Змушує будівельника додавати деталі
        public void ConstructLaptop()
        {
            _laptopBuilder.CreateNewLaptop();
            _laptopBuilder.SetMonitorResolution();
            _laptopBuilder.SetProcessor();
            _laptopBuilder.SetMemory();
            _laptopBuilder.SetHDD();
            _laptopBuilder.SetBattery();
        }
    }

    public class Laptop
    {
        public string MonitorResolution { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string HDD { get; set; }
        public string Battery { get; set; }

        public override string ToString()
        {
            return String.Format("Laptop: {0}, {1}, {2}, {3}, {4}", MonitorResolution, Processor, Memory, HDD, Battery);
        }
    }


    /* Client Code */
    public class BuilderDemo
    {
        public static void Run()
        {
            //Your system could have bulk of builders
            var tripBuilder = new TripLaptopBuilder();
            var gamingBuilder = new GamingLaptopBuilder();
            var shopForYou = new BuyLaptop();//director
            shopForYou.SetLaptopBuilder(gamingBuilder);//Customer answered that he wants to play
            shopForYou.ConstructLaptop();
            Laptop laptop = shopForYou.GetLaptop();//just get what he wants
            Console.WriteLine(laptop.ToString());
        }
    }
}
