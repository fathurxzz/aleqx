using System;

namespace SpaceGame.ConsoleTest
{
    public static class Extensions
    {
        public static string FormatResourcesAmount(this string source)
        {
            for (int i = source.Length; i > 0; i--)
            {
                if ((source.Length - i + 1) % 4 == 0)
                {
                    source = source.Insert(i, ".");
                }
            }
            return source;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //string amount = "1234567890";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "123456789";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "12345678";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "1234567";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "123456";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "12345";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "1234";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "123";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "12";
            //Console.WriteLine(amount.FormatResourcesAmount());
            //amount = "1";
            //Console.WriteLine(amount.FormatResourcesAmount());


            for (int i = 1; i <= 20; i++)
            {
                var reduction = Math.Max(4 - i / 2.0, 1);
                Console.WriteLine( 3600 * (400*i + 200*i) / (2500.0 * reduction * (0 + 1.0) * Math.Pow(2.0, 0)));
            }
        }
    }
}
