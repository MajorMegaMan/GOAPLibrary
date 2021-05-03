using System.Collections.Generic;

namespace GOAP
{
    public static class GOAPPlanner<GameObjectRef>
    {
        static bool foundGoal = false;
        static int lowCost = int.MaxValue;
        static List<Node> outcomeTree = new List<Node>();
        static List<Node> finalGoals = new List<Node>();

        static int runningTotalCap = 20;

        static void Reset()
        {
            foundGoal = false;
            lowCost = int.MaxValue;
            outcomeTree.Clear();
            finalGoals.Clear();
        }

        public static Queue<GOAPAction<GameObjectRef>> CalcPlan(GOAPWorldState currentWorldstate, GOAPWorldState goal, List<GOAPAction<GameObjectRef>> actions)
        {
            if (goal == null)
            {
                return new Queue<GOAPAction<GameObjectRef>>();
            }

            Reset();

            //List<Node> outcomeTree = new List<Node>();
            Node start = new Node(null, 0, currentWorldstate, null);
            outcomeTree.Add(start);

            //List<Node> finalGoals = new List<Node>();

            for (int i = 0; i < outcomeTree.Count; i++)
            {
                if (!outcomeTree[i].isGoal)
                {
                    BuildTree(outcomeTree[i], goal, actions);
                }
            }

            Node smallestCost = null;
            if (finalGoals.Count > 0)
            {
                smallestCost = finalGoals[0];
            }
            else
            {
                return new Queue<GOAPAction<GameObjectRef>>();
            }

            for (int i = 0; i < finalGoals.Count; i++)
            {
                if (finalGoals[i].runningTotal < smallestCost.runningTotal)
                {
                    smallestCost = finalGoals[i];
                }
            }

            List<GOAPAction<GameObjectRef>> planList = new List<GOAPAction<GameObjectRef>>();

            Node current = smallestCost;
            while (current != start && current != null)
            {
                // Add to front of list
                planList.Insert(0, current.action);
                current = current.parent;
            }

            Queue<GOAPAction<GameObjectRef>> result = new Queue<GOAPAction<GameObjectRef>>();
            foreach (var act in planList)
            {
                result.Enqueue(act);
            }

            return result;
        }

        public static List<GOAPAction<GameObjectRef>> GetUsableActions(GOAPWorldState currentWorldstate, List<GOAPAction<GameObjectRef>> actions)
        {
            List<GOAPAction<GameObjectRef>> usableActions = new List<GOAPAction<GameObjectRef>>();
            foreach (var act in actions)
            {
                if (currentWorldstate.CheckState(act.GetPreconditions()))
                {
                    usableActions.Add(act);
                }
            }

            return usableActions;
        }

        static void BuildTree(Node node, GOAPWorldState goal, List<GOAPAction<GameObjectRef>> actions)
        {
            List<GOAPAction<GameObjectRef>> usableActions = GetUsableActions(node.currentState, actions);

            foreach (var act in usableActions)
            {
                if (foundGoal && lowCost < node.runningTotal || node.runningTotal > runningTotalCap)
                {
                    break;
                }
                GOAPWorldState affectedState = new GOAPWorldState(node.currentState);
                act.AddEffects(affectedState);

                Node newLeaf = new Node(node, node.runningTotal + act.GetWeight(), affectedState, act);
                //outcomeTree.Add(newLeaf);

                if (affectedState.CheckState(goal))
                {
                    // this is our target goal
                    finalGoals.Add(newLeaf);
                    newLeaf.isGoal = true;
                    foundGoal = true;
                    if (newLeaf.runningTotal < lowCost)
                    {
                        lowCost = newLeaf.runningTotal;
                    }
                }
                else
                {
                    // keep searching
                    outcomeTree.Add(newLeaf);
                }
            }
            // it is possible that no usable actions were found. and in this case the tree will stop building.
            // Possibly from the very first node
        }

        class Node
        {
            public Node parent;
            public int runningTotal;
            public GOAPWorldState currentState;
            // The action that was performed to get to this worldstate
            public GOAPAction<GameObjectRef> action;
            public bool isGoal = false;

            public Node(Node parent, int runningTotal, GOAPWorldState currentState, GOAPAction<GameObjectRef> action)
            {
                this.parent = parent;
                this.runningTotal = runningTotal;
                this.currentState = currentState;
                this.action = action;
            }
        }
    }
}
