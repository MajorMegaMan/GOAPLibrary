using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram
{
    abstract class NonAgentAction : GOAPAction<FakeGameObject>
    {
        public override ActionState PerformAction(GOAPAgent<FakeGameObject> agent, GOAPWorldState worldState)
        {
            return ActionState.completed;
        }

        public override bool EnterAction(GOAPAgent<FakeGameObject> agent)
        {
            return true;
        }

        public override bool IsInRange(GOAPAgent<FakeGameObject> agent)
        {
            return true;
        }
    }
}
