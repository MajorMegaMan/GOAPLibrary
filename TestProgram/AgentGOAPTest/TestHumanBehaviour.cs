using System;
using System.Collections.Generic;
using System.Text;

using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class TestHumanBehaviour : GOAPBehaviour<FakeGameObject>
    {
        public TestHumanBehaviour()
        {
            m_actions.Add(new PickUpWood());
        }

        public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
        {
            return null;
        }
    }
}
