using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using GOAP;



using T_Action = GOAP.GOAPAction<TestProgram.FakeGameObject>;
using T_WorldState = GOAP.GOAPWorldState;
using T_Planner = GOAP.GOAPPlanner<TestProgram.FakeGameObject>;


namespace TestProgram
{
    class Manager
    {
        public T_WorldState worldState;
        public T_WorldState goal;

        public List<T_Action> actions;
        public Queue<T_Action> plan;

        Stopwatch timer;

        public Manager()
        {
            worldState = InitWorldState();
            goal = InitGoalState();

            actions = InitActions();

            timer = Stopwatch.StartNew();

            Console.WriteLine("before plan : " + timer.ElapsedMilliseconds);
            plan = T_Planner.CalcPlan(worldState, goal, actions);
            Console.WriteLine("after plan : " + timer.ElapsedMilliseconds);
            timer.Stop();
        }

        T_WorldState InitWorldState()
        {
            T_WorldState worldState = new T_WorldState();

            worldState.CreateElement(WorldValues.storedWood, 0);
            worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            return worldState;
        }

        T_WorldState InitGoalState()
        {
            T_WorldState goal = new T_WorldState();

            goal.CreateElement(WorldValues.storedWood, 1);
            goal.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            return goal;
        }

        List<T_Action> InitActions()
        {
            List<T_Action> actions = new List<T_Action>();

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
