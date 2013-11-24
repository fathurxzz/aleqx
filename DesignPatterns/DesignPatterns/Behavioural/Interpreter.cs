// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{
    // Context
    class CurrentPricesContext
    {
        Dictionary<string, int> _prices = new Dictionary<string, int>();

        public CurrentPricesContext()
        {
            _prices.Add("Bed", 3000);
            _prices.Add("TV", 400);
            _prices.Add("Laptop", 1500);
        }

        public int GetPrice(string goodName)
        {
            if (_prices.ContainsKey(goodName))
            {
                return _prices[goodName];
            }
            else
            {
                throw new ArgumentException("Could not get price for the good that is not registered.");
            }
        }

        public void SetPrice(string goodName, int cost)
        {
            if (_prices.ContainsKey(goodName))
            {
                _prices[goodName] = cost;
            }
            else
            {
                _prices.Add(goodName, cost);
            }
        }
    }

    // Abstract expression
    abstract class Goods
    {
        public abstract int Interpret(CurrentPricesContext context);
    }
    // Nonterminal expression
    class GoodsPackage : Goods
    {
        public List<Goods> GoodsInside { get; set; }
        public override int Interpret(CurrentPricesContext context)
        {
            var totalSum = 0;
            foreach (var goods in GoodsInside)
            {
                totalSum += goods.Interpret(context);
            }
            return totalSum;
        }
    }
    // Terminal expression
    class TV : Goods
    {
        public override int Interpret(CurrentPricesContext context)
        {
            int price = context.GetPrice("TV");
            Console.WriteLine("TV: {0}", price);
            return price;
        }
    }

    // Terminal expression
    class Laptop : Goods
    {
        public override int Interpret(CurrentPricesContext context)
        {
            int price = context.GetPrice("Laptop");
            Console.WriteLine("Laptop: {0}", price);
            return price;
        }
    }

    // Terminal expression
    class Bed : Goods
    {
        public override int Interpret(CurrentPricesContext context)
        {
            int price = context.GetPrice("Bed");
            Console.WriteLine("Bed: {0}", price);
            return price;
        }
    }

    class InterpreterDemo
    {
        public static void Run()
        {
            new InterpreterDemo().RunInterpreterDemo();
        }

        public void RunInterpreterDemo()
        {
            // create syntax tree that represents sentence
            var truckWithGoods = PrepareTruckWithGoods();
            // get latest context
            var pricesContext = GetRecentPricesContext();
            // invoke Interpret
            var totalPriceForGoods = truckWithGoods.Interpret(pricesContext);

            Console.WriteLine("Total: {0}", totalPriceForGoods);
        }

        private CurrentPricesContext GetRecentPricesContext()
        {
            var pricesContext = new CurrentPricesContext();
            pricesContext.SetPrice("Bed", 400);
            pricesContext.SetPrice("TV", 100);
            pricesContext.SetPrice("Laptop", 500);
            return pricesContext;
        }

        public GoodsPackage PrepareTruckWithGoods()
        {
            var truck = new GoodsPackage() { GoodsInside = new List<Goods>() };

            var bed = new Bed();
            var doubleTriplePackedBed = new GoodsPackage()
                {
                    GoodsInside = new List<Goods>() { new GoodsPackage() { GoodsInside = new List<Goods>() { bed } } }
                };
            truck.GoodsInside.Add(doubleTriplePackedBed);
            truck.GoodsInside.Add(new TV());
            truck.GoodsInside.Add(new TV());
            truck.GoodsInside.Add(new GoodsPackage()
                {
                    GoodsInside = new List<Goods>() { new Laptop(), new Laptop(), new Laptop() }
                });

            return truck;
        }
    }
}