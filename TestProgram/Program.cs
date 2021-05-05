using System;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BaseGOAPTest.BaseGOAPManager manager = new BaseGOAPTest.BaseGOAPManager();

            manager.Print();

            Console.ReadLine();
        }
    }
}
