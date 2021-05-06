using System;

namespace TestProgram
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //BaseGOAPTest.BaseGOAPManager manager = new BaseGOAPTest.BaseGOAPManager();
            AgentGOAPTest.AgentGOAPManager manager = new AgentGOAPTest.AgentGOAPManager();

            manager.Run();
            return 0;
        }
    }
}
