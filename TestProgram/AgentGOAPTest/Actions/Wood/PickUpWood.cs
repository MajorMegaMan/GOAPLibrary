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
            preconditions.CreateElement(WorldValues.woodAvailable, true);

            effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);
            effects.CreateElement(WorldValues.woodAvailable, false); // This is a possible effect
            effects.CreateElement(WorldValues.worldWoodCount, -1);

            name = "Pick Up Wood";
        }

        public override void AddEffects(GOAPWorldState state)
        {
            state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.wood);

            var woodCountData = state.GetData(WorldValues.worldWoodCount);
            int value = woodCountData.ConvertValue<int>();
            value--;
            woodCountData.value = value;

            bool isWoodAvailable = value > 0;
            state.SetElementValue(WorldValues.woodAvailable, isWoodAvailable);
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
