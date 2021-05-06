using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class PickUpAxe : GOAPAgentAction<FakeGameObject>
    {
        public PickUpAxe()
        {
            preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

            name = "Pick Up Axe";
        }

        public override void AddEffects(GOAPWorldState state)
        {
            state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.axe);
        }

        public override bool EnterAction(GOAPAgent<FakeGameObject> agent)
        {
            return true;
        }

        public override ActionState PerformAction(GOAPAgent<FakeGameObject> agent, GOAPWorldState worldState)
        {
            return ActionState.completed;
        }

        public override bool IsInRange(GOAPAgent<FakeGameObject> agent)
        {
            return true;
        }
    }
}
