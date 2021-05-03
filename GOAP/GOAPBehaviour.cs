using System.Collections.Generic;

namespace GOAP
{
    public abstract class GOAPBehaviour<GameObjectRef>
    {
        protected List<GOAPAction<GameObjectRef>> m_actions = new List<GOAPAction<GameObjectRef>>();
        protected GOAPWorldState m_selfishNeeds = new GOAPWorldState();

        public GOAPBehaviour()
        {
            // Initialise Action List
        }

        public abstract GOAPWorldState FindGoal(GOAPWorldState agentWorldState);

        public List<GOAPAction<GameObjectRef>> GetActions()
        {
            return m_actions;
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

        public Queue<GOAPAction<GameObjectRef>> CalcPlan(GOAPWorldState agentWorldState)
        {
            return GOAPPlanner<GameObjectRef>.CalcPlan(agentWorldState, FindGoal(agentWorldState), GetActions());
        }
    }

}
