using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram
{
    class AIAgent
    {
        FakeGameObject gameObject = new FakeGameObject();
        GOAPAgent<FakeGameObject> m_goapAgent;

        public AIAgent()
        {
            m_goapAgent = new GOAPAgent<FakeGameObject>(gameObject);
        }

        public void Init(GOAPBehaviour<FakeGameObject> behaviour, GOAPWorldState worldState)
        {
            m_goapAgent.SetBehaviour(behaviour);
            m_goapAgent.SetWorldState(worldState);
        }

        public GOAPWorldState GetAgentWorldstate()
        {
            return m_goapAgent.GetWorldState();
        }

        public Queue<GOAPAgentAction<FakeGameObject>> GetPlan()
        {
            return m_goapAgent.GetPlan();
        }

        public Queue<GOAPAgentAction<FakeGameObject>> FindAndGetPlan()
        {
            m_goapAgent.FindPlan();
            return GetPlan();
        }

        public void Update()
        {
            m_goapAgent.Update();
        }

        public GOAPAction GetAction()
        {
            return m_goapAgent.GetAction();
        }
    }
}
