using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class StoreWood : GOAPAgentAction<FakeGameObject>
    {
        public StoreWood()
        {
            preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);

            effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
            effects.CreateElement(WorldValues.storedWood, +1);

            name = "Store Wood";
        }

        public override void AddEffects(GOAPWorldState state)
        {
            state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
            int woodCount = state.GetElementValue<int>(WorldValues.storedWood);
            state.SetElementValue(WorldValues.storedWood, woodCount + 1);
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
