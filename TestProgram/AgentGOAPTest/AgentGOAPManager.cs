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
            Console.Clear();
            testAgent.Update();
        }

        public void Print()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("Agent World State");

            Console.WriteLine("===============================================");

            PrintPlanningTree();

            var currentAction = testAgent.GetAction();
            if (currentAction != null)
            {
                Console.WriteLine("Current action: " + currentAction.GetName());
                currentAction.AddEffects(testAgent.GetAgentWorldstate());
            }
            else
            {
                Console.WriteLine("Current action: planning");
            }

            PrintWorldState(testAgent.GetAgentWorldstate());
            PrintUsableActions();
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
                Console.WriteLine(actionIndex++ + ". Planned action: " + action.GetName());
                //action.AddEffects(agentWorldstate);
                //PrintWorldState(agentWorldstate);
                //Console.WriteLine();
            }
            Console.WriteLine("End of Plan");
        }

        public void PrintUsableActions()
        {
            Console.WriteLine("     UsableActions");
            Console.WriteLine();
            Console.Write("         ");

            var actions = testAgent.GetUsableActions();

            foreach(var act in actions)
            {
                Console.Write(act.GetName() + ", ");
            }
            Console.WriteLine();
        }

        public void PrintPlanningTree()
        {
            var tree = GOAPPlanner.DebugBuildTree(testAgent.GetAgentWorldstate(), behaviours[0].FindGoal(testAgent.GetAgentWorldstate()), behaviours[0].GetBaseActions());

            for(int i = 1; i < tree.Count; i++)
            {
                var node = tree[i]; 
            
                Console.Write(node.depth + " : " + node.action.GetName());
            
                if(node.isGoal)
                {
                    Console.WriteLine(" --- GOAL");
                }
                else
                {
                    Console.WriteLine();
                }
            
                Console.WriteLine();
            
                var parent = node.parent;
                while(parent != null)
                {
                    if(parent.action != null)
                    {
                        Console.Write(parent.action.GetName() + ", ");
                    }
                    parent = parent.parent;
                }
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------");
            }

        }
    }
}
