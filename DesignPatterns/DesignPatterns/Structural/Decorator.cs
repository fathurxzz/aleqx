// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Structural
{
    class Car
    {
        protected String BrandName { get; set; }
        public virtual void Go()
        {
            Console.WriteLine("I'm " + BrandName + " and I'm on my way...");
        }
    }

    class DecoratorCar : Car
    {
        protected Car DecoratedCar { get; set; }
        public DecoratorCar(Car decoratedCar)
        {
            DecoratedCar = decoratedCar;
        }
        public override void Go()
        {
            DecoratedCar.Go();
        }
    }

    class AmbulanceCar : DecoratorCar
    {
        public AmbulanceCar(Car decoratedCar)
            : base(decoratedCar)
        {
        }
        public override void Go()
        {
            base.Go();
            Console.WriteLine("... beep-beep-beeeeeep ...");
        }
    }
    //конкретна реалізація класу Car
    class Mersedes : Car
    {
        public Mersedes()
        {
            BrandName = "Mersedes";
        }
    }

    public static class DecoratorDemo
    {
        public static void Run()
        {
            var doctorsDream = new AmbulanceCar(new Mersedes());
            doctorsDream.Go();
        }
    }
}
