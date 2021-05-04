using System;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Manager manager = new Manager();

            manager.Print();

            Console.ReadLine();
        }
    }
}
