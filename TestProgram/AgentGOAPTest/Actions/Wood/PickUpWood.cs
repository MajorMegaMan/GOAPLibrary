using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class PickUpWood : GOAPAgentAction<FakeGameObject>
    {
        public PickUpWood()
        {
            preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);

            effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);

            name = "Pick Up Wood";
        }

        public override void AddEffects(GOAPWorldState state)
        {
            state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.wood);
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
