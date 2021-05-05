using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram.BaseGOAPTest
{
    class StoreWood : GOAPAction
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
    }
}