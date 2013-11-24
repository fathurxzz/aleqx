// Book "������ ������� - ������, �� ����" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// �� ���� ��������� � ��������� ����.
// ������ ����� ��� �� ��� ���, ����� �� ��������� ��� �������� �� ������ ������� �� ��� �����.
using System;

namespace DesignPatterns.Creational
{
    // abstract factory
    // ��������� �������
    public interface IToyFactory
    {
        Bear GetBear();
        Cat GetCat();
    }
    // concrete factory
    // ��������� �������
    public class TeddyToysFactory : IToyFactory
    {
        public Bear GetBear()
        {
            return new TeddyBear();
        }
        public Cat GetCat()
        {
            return new TeddyCat();
        }
    }
    // concrete factory
    // ��������� �������
    public class WoodenToysFactory : IToyFactory
    {
        public Bear GetBear()
        {
            return new WoodenBear();
        }
        public Cat GetCat()
        {
            return new WoodenCat();
        }
    }

    public abstract class AnimalToy
    {
        protected AnimalToy(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
    public abstract class Cat : AnimalToy
    {
        protected Cat(string name) : base(name) { }
    }
    public abstract class Bear : AnimalToy
    {
        protected Bear(string name) : base(name) { }
    }
    class WoodenCat : Cat
    {
        public WoodenCat() : base("Wooden Cat") { }
    }
    class TeddyCat : Cat
    {
        public TeddyCat() : base("Teddy Cat") { }
    }
    class WoodenBear : Bear
    {
        public WoodenBear() : base("Wooden Bear") { }
    }
    class TeddyBear : Bear
    {
        public TeddyBear() : base("Teddy Bear") { }
    }

    public class AbstractFactoryDemo
    {
        public static void Run()
        {
            RunWoodenFactory();
            RunTeddyFactory();
        }

        private static void RunWoodenFactory()
        {
            // lets start with wooden factory
            // �������� �������� �����'��� �������
            IToyFactory toyFactory = new WoodenToysFactory();

            Bear bear = toyFactory.GetBear();
            Cat cat = toyFactory.GetCat();
            Console.WriteLine("I've got {0} and {1}", bear.Name, cat.Name);
            // Output/����: [I've got Wooden Bear and Wooden Cat]
        }

        private static void RunTeddyFactory()
        {
            // and now teddy one
            // � ����� �������
            IToyFactory toyFactory = new TeddyToysFactory();

            Bear bear = toyFactory.GetBear();
            Cat cat = toyFactory.GetCat();
            Console.WriteLine("I've got {0} and {1}", bear.Name, cat.Name);
            // Output/����: [I've got Teddy Bear and Teddy Cat]
        }
    }
}