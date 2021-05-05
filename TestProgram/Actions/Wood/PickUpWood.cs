using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram
{
    class PickUpWood : GOAPAction
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
    }
}
