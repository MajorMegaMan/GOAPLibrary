using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class DropAxe : GOAPAgentAction<FakeGameObject>
    {
        public DropAxe()
        {
            preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

            effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            name = "Drop Axe";
        }

        public override void AddEffects(GOAPWorldState state)
        {
            state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
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
