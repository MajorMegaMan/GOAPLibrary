using System.Collections.Generic;

namespace GOAP
{
    public abstract class GOAPBehaviour<GameObjectRef>
    {
        protected List<GOAPAgentAction<GameObjectRef>> m_actions = new List<GOAPAgentAction<GameObjectRef>>();
        protected GOAPWorldState m_selfishNeeds = new GOAPWorldState();

        public GOAPBehaviour()
        {
            // Initialise Action List
        }

        public abstract GOAPWorldState FindGoal(GOAPWorldState agentWorldState);

        public List<GOAPAgentAction<GameObjectRef>> GetActions()
        {
            return m_actions;
        }

        List<GOAPAction> GetBaseActions()
        {
            List<GOAPAction> baseActions = new List<GOAPAction>();
            foreach(var act in m_actions)
            {
                baseActions.Add(act);
            }

            return baseActions;
        }

        public virtual void Update(GOAPAgent<GameObjectRef> agent, GOAPWorldState agentSelfishNeeds)
        {
            // This is purposely empty just so this can be called without errors.
            // This should be overridden if you want inherited behaviours to have logic outside of planning aswell.
            // eg. A human gets hungry while chopping wood and rather than finishing get wood, he will immediatly get a plan for getting food
        }

        public GOAPWorldState GetSelfishNeeds()
        {
            return new GOAPWorldState(m_selfishNeeds);
        }

        public Queue<GOAPAgentAction<GameObjectRef>> CalcPlan(GOAPWorldState agentWorldState)
        {
            Queue<GOAPAction> plan = GOAPPlanner.CalcPlan(agentWorldState, FindGoal(agentWorldState), GetBaseActions());
            Queue<GOAPAgentAction<GameObjectRef>> agentPlan = new Queue<GOAPAgentAction<GameObjectRef>>();
            while(plan.Count > 0)
            {
                agentPlan.Enqueue((GOAPAgentAction<GameObjectRef>)plan.Dequeue());
            }

            return agentPlan;
        }
    }

}
