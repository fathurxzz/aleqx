// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;

namespace DesignPatterns.Structural
{
    // Клас який буде адаптовуватися
    class OldElectricitySystem
    {
        public string MatchThinSocket()
        {
            return "220V";
        }
    }
    // широковикористовуваний інтерфейс
    interface INewElectricitySystem
    {
        string MatchWideSocket();
    }
    class NewElectricitySystem : INewElectricitySystem
    {
        public string MatchWideSocket()
        {
            return "220V";
        }
    }
    // Адаптер
    class Adapter : INewElectricitySystem
    {
        private readonly OldElectricitySystem _adaptee;
        public Adapter(OldElectricitySystem adaptee)
        {
            _adaptee = adaptee;
        }
        // А тут коїться вся магія 
        // наш адаптер перекладає із того, що ми (код) не можемо використати наразі у те що ми можемо
        public string MatchWideSocket()
        {
            return _adaptee.MatchThinSocket();
        }
    }

    class ElectricityConsumer
    {
        // Зарядний пристрій розуміє тільки нову систему
        public static void ChargeNotebook(INewElectricitySystem electricitySystem)
        {
            Console.WriteLine(electricitySystem.MatchWideSocket());
        }
    }

    public class AdapterDemo
    {
        public static void Run()
        {
            // 1)
            // Ми можемо і надалі користувати нашою новою системою
            var newElectricitySystem = new NewElectricitySystem();
            ElectricityConsumer.ChargeNotebook(newElectricitySystem);

            // 2)
            // Ми повинні адаптуватися до старої системи, використовуючи адаптер
            var oldElectricitySystem = new OldElectricitySystem();
            var adapter = new Adapter(oldElectricitySystem);
            ElectricityConsumer.ChargeNotebook(adapter);
        }
    }
}