// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{
    abstract class WierdCafeVisitor
    {
        public WierdCafeVisitor CafeVisitor { get; private set; }
        protected WierdCafeVisitor(WierdCafeVisitor cafeVisitor)
        {
            CafeVisitor = cafeVisitor;
        }
        public virtual void HandleFood(Food food)
        {
            // If I cannot handle other food, passing it to my successor
            if (CafeVisitor != null)
            {
                CafeVisitor.HandleFood(food);
            }
        }
    }

    class GirlFriend : WierdCafeVisitor
    {
        public GirlFriend(WierdCafeVisitor cafeVisitor)
            : base(cafeVisitor)
        {
        }

        public override void HandleFood(Food food)
        {
            if (food.Name == "Cappuccino")
            {
                Console.WriteLine("GirlFriend: My lovely cappuccino!!!");
                return;
            }
            base.HandleFood(food);
        }
    }

    class Me : WierdCafeVisitor
    {
        public Me(WierdCafeVisitor cafeVisitor)
            : base(cafeVisitor)
        {
        }

        public override void HandleFood(Food food)
        {
            if (food.Name.Contains("Soup"))
            {
                Console.WriteLine("Me: I like Soup. It went well.");
            }
            base.HandleFood(food);
        }
    }

    class BestFriend : WierdCafeVisitor
    {
        public List<Food> CoffeeContainingFood { get; private set; }
        public BestFriend(WierdCafeVisitor cafeVisitor)
            : base(cafeVisitor)
        {
            CoffeeContainingFood = new List<Food>();
        }

        public override void HandleFood(Food food)
        {
            if (food.Ingradients.Contains("Meat"))
            {
                Console.WriteLine("BestFriend: I just ate {0}. It was testy.", food.Name);
                return;
            }
            if (food.Ingradients.Contains("Coffee") && CoffeeContainingFood.Count < 1)
            {
                CoffeeContainingFood.Add(food);
                Console.WriteLine("BestFriend: I have to take something with coffee. {0} looks fine.", food.Name);
                return;
            }
            base.HandleFood(food);
        }
    }

    class Food
    {
        public Food(string name, List<string> ingradients)
        {
            Name = name;
            Ingradients = ingradients;
        }

        public List<string> Ingradients { get; private set; }

        public string Name { get; private set; }
    }

    class ChainOfResponsibility
    {
        public static void Run()
        {
            var cappuccino1 = new Food("Cappuccino", new List<string> { "Coffee", "Milk", "Sugar" });
            var cappuccino2 = new Food("Cappuccino", new List<string> { "Coffee", "Milk" });
            var soup1 = new Food("Soup with meat", new List<string> { "Meat", "Water", "Potato" });
            var soup2 = new Food("Soup with potato", new List<string> { "Water", "Potato" });
            var meat = new Food("Meat", new List<string> { "Meat" });

            var girlFriend = new GirlFriend(null);
            var me = new Me(girlFriend);
            var bestFriend = new BestFriend(me);

            bestFriend.HandleFood(cappuccino1);
            bestFriend.HandleFood(cappuccino2);
            bestFriend.HandleFood(soup1);
            bestFriend.HandleFood(soup2);
            bestFriend.HandleFood(meat);
        }
    }
}