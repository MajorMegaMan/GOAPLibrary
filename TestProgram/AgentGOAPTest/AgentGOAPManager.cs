using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class AgentGOAPManager
    {
        GOAPWorldState worldState;

        List<GOAPBehaviour<FakeGameObject>> behaviours = new List<GOAPBehaviour<FakeGameObject>>();

        AIAgent testAgent = new AIAgent();

        public AgentGOAPManager()
        {
            worldState = new GOAPWorldState();

            // wood Values
            worldState.CreateElement(WorldValues.storedWood, 0);
            worldState.CreateElement(WorldValues.woodAvailable, false);
            worldState.CreateElement(WorldValues.worldWoodCount, 0);

            behaviours.Add(new TestHumanBehaviour());

            testAgent.Init(behaviours[0], worldState);
        }

        public void Run()
        {
            while(true)
            {
                Update();
                Print();

                // Pause effect
                Console.ReadLine();
            }
        }

        void Update()
        {
            testAgent.Update();
        }

        public void Print()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("Agent World State");

            Console.WriteLine("===============================================");

            var currentAction = testAgent.GetAction();
            if (currentAction != null)
            {
                Console.WriteLine("Current action: " + currentAction.GetName());
                currentAction.AddEffects(testAgent.GetAgentWorldstate());
            }
            else
            {
                Console.WriteLine("Current action: null");
            }

            PrintWorldState(testAgent.GetAgentWorldstate());
            Console.WriteLine();
            Console.WriteLine("===============================================");
            PrintPlan(testAgent.GetPlan(), testAgent.GetAgentWorldstate());
        }

        public void PrintWorldState(GOAPWorldState worldState)
        {
            Console.WriteLine("-----------------------------------------------");
            var needs = worldState.GetNames();
            foreach(var name in needs)
            {
                Console.WriteLine("     " + name + ": " + worldState.GetElementValue(name));
            }
            Console.WriteLine("-----------------------------------------------");
        }

        public void PrintPlan(Queue<GOAPAgentAction<FakeGameObject>> agentPlan, GOAPWorldState agentWorldstate)
        {
            Console.WriteLine("Start of Plan");
            Console.WriteLine("===============================================");

            int actionIndex = 0;

            while (agentPlan.Count > 0)
            {
                var action = agentPlan.Dequeue();
                Console.WriteLine(actionIndex++ + ". Performing action: " + action.GetName());
                action.AddEffects(agentWorldstate);
                PrintWorldState(agentWorldstate);
                Console.WriteLine();
            }
            Console.WriteLine("End of Plan");
        }
    }
}
