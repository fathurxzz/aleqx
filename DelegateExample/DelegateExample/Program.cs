using System;

namespace DelegateExample
{
    delegate double MathAction(double a, double b);

    class Program
    {
        static double Add(double a, double b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {

            MathAction ma1 = new MathAction(Add);
            double result1 = ma1(5, 6);
            Console.WriteLine(result1);


            MathAction ma2 = delegate(double a, double b)
            {
                return a * b;
            };
            double result2 = ma2(7, 8);
            Console.WriteLine(result2);


            MathAction ma3 = (s, a) => (s * s * a * a);
            double result3 = ma3(4, 4);
            Console.WriteLine(result3);

        }
    }
}
