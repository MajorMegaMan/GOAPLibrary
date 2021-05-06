using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.AgentGOAPTest
{
    class ChopWood : GOAPAgentAction<FakeGameObject>
    {
        public ChopWood()
        {
            preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

            effects.CreateElement(WorldValues.woodAvailable, true);
            effects.CreateElement(WorldValues.worldWoodCount, +1);

            name = "Chop Wood";
        }

        public override void AddEffects(GOAPWorldState state)
        {
            state.SetElementValue(WorldValues.woodAvailable, true);

            var woodCountData = state.GetData(WorldValues.worldWoodCount);
            int value = woodCountData.ConvertValue<int>();
            value++;
            woodCountData.value = value;
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
