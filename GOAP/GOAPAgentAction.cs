using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOAP
{
    public abstract class GOAPAgentAction<GameObjectRef> : GOAPAction
    {
        public abstract ActionState PerformAction(GOAPAgent<GameObjectRef> agent, GOAPWorldState worldState);

        public abstract bool EnterAction(GOAPAgent<GameObjectRef> agent);

        public abstract bool IsInRange(GOAPAgent<GameObjectRef> agent);

        public virtual bool CanPerformAction(GOAPAgent<GameObjectRef> agent, GOAPWorldState worldState)
        {
            return worldState.CheckState(preconditions);
        }
    }
}
