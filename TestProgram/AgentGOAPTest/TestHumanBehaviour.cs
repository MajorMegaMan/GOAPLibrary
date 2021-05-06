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
            // Initialise Action List
            m_actions.Add(new PickUpWood());
            m_actions.Add(new StoreWood());
            m_actions.Add(new ChopWood());

            m_actions.Add(new PickUpAxe());
            m_actions.Add(new DropAxe());

            // Initialise WorldStateNeeds
            m_selfishNeeds.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        }

        public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
        {
            GOAPWorldState goal = new GOAPWorldState();

            int value = agentWorldState.GetElementValue<int>(WorldValues.storedWood);
            value += 1;
            goal.CreateElement(WorldValues.storedWood, value);

            return goal;
        }
    }
}
