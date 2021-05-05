using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using GOAP;

namespace TestProgram
{
    class Manager
    {
        public GOAPWorldState worldState;
        public GOAPWorldState goal;

        public List<GOAPAction> actions;
        public Queue<GOAPAction> plan;

        Stopwatch timer;

        public Manager()
        {
            worldState = InitWorldState();
            goal = InitGoalState();

            actions = InitActions();

            timer = Stopwatch.StartNew();

            Console.WriteLine("before plan : " + timer.ElapsedMilliseconds);
            plan = GOAPPlanner.CalcPlan(worldState, goal, actions);
            Console.WriteLine("after plan : " + timer.ElapsedMilliseconds);
            timer.Stop();
        }

        GOAPWorldState InitWorldState()
        {
            GOAPWorldState worldState = new GOAPWorldState();

            worldState.CreateElement(WorldValues.storedWood, 0);
            worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            return worldState;
        }

        GOAPWorldState InitGoalState()
        {
            GOAPWorldState goal = new GOAPWorldState();

            goal.CreateElement(WorldValues.storedWood, 1);
            goal.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            return goal;
        }

        List<GOAPAction> InitActions()
        {
            List<GOAPAction> actions = new List<GOAPAction>();

            actions.Add(new PickUpWood());
            actions.Add(new StoreWood());

            return actions;
        }

        public void Print()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("World State");
            PrintWorldState();
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("Start of Plan");
            Console.WriteLine("===============================================");

            while (plan.Count > 0)
            {
                var action = plan.Dequeue();
                Console.ReadLine();
                Console.WriteLine("Performing action: " + action.GetName());
                action.AddEffects(worldState);
                PrintWorldState();
                Console.WriteLine();
            }
            Console.WriteLine("End of Plan");
        }

        public void PrintWorldState()
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("     Holding item: " + worldState.GetElementValue<WorldValues.HoldItemType>(WorldValues.holdItemType));
            Console.WriteLine("      Stored wood: " + worldState.GetElementValue<int>(WorldValues.storedWood));
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
