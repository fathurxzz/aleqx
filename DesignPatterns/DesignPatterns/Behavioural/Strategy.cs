// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{
    interface IWearingStrategy
    {
        string GetClothes();
        string GetAccessories();
    }
    class SunshineWearingStrategy : IWearingStrategy
    {
        public string GetClothes()
        {
            return "T-Shirt";
        }
        public string GetAccessories()
        {
            return "sunglasses";
        }
    }

    class RainWearingStrategy : IWearingStrategy
    {
        public string GetClothes()
        {
            return "Coat";
        }
        public string GetAccessories()
        {
            return "umbrella";
        }
    }

    class Myself
    {
        private IWearingStrategy _wearingStrategy = new DefaultWearingStrategy();

        public void ChangeStrategy(IWearingStrategy wearingStrategy)
        {
            _wearingStrategy = wearingStrategy;
        }
        public void GoOutside()
        {
            var clothes = _wearingStrategy.GetClothes();
            var accessories = _wearingStrategy.GetAccessories();
            Console.WriteLine("Today I wore {0} and took {1}", clothes, accessories);
        }
    }

    class DefaultWearingStrategy : IWearingStrategy
    {
        public string GetClothes()
        {
            return "whateveryoulike";
        }
        public string GetAccessories()
        {
            return "nothing";
        }
    }

    class Weather
    {
        public static string GetWeather()
        {
            return "rain";
        }
    }

    public static class StrategyDemo
    {
        public static void Run()
        {
            //var weather = Weather.GetWeather();
            //IWearingStrategy wearingStrategy = new DefaultWearingStrategy();
            //if (weather == "rain")
            //{
            //    wearingStrategy = new RainWearingStrategy();
            //}

            var me = new Myself();
            me.ChangeStrategy(new RainWearingStrategy());
            me.GoOutside();
        }
    }


    class Myself_BeforeStrategyDP
    {
        public void GoOutside()
        {
            var weather = Weather.GetWeather();
            string clothes = GetClothes(weather);
            string accessories = GetAccessories(weather);
            Console.WriteLine("Today I wore {0} and took {1}", clothes, accessories);
        }

        private string GetAccessories(string weather)
        {
            string accessories;
            switch (weather)
            {
                case "sun":
                    accessories = "sunglasses";
                    break;
                case "rain":
                    accessories = "umbrella";
                    break;
                default:
                    accessories = "nothing";
                    break;
            }
            return accessories;
        }

        private string GetClothes(string weather)
        {
            string clothes;
            switch (weather)
            {
                case "sun":
                    clothes = "T-Shirt";
                    break;
                case "rain":
                    clothes = "Coat";
                    break;
                default:
                    clothes = "Shirt";
                    break;
            }
            return clothes;
        }
    }
}