using System.Collections.Generic;

namespace GOAP
{
    public abstract class GOAPAction<GameObjectRef>
    {

        protected GOAPWorldState preconditions;
        protected GOAPWorldState effects;

        protected string name;

        protected int weight = 1;

        public enum ActionState
        {
            completed,
            performing,
            interrupt
        }

        public GOAPAction()
        {
            preconditions = new GOAPWorldState();
            effects = new GOAPWorldState();
        }

        public GOAPWorldState GetPreconditions()
        {
            GOAPWorldState result = new GOAPWorldState(preconditions);
            return result;
        }

        public GOAPWorldState GetEffects()
        {
            GOAPWorldState result = new GOAPWorldState(effects);
            return result;
        }

        public string GetName()
        {
            return name;
        }

        public int GetWeight()
        {
            return weight;
        }

        public string[] GetPreconNames()
        {
            List<string> names = preconditions.GetNames();

            string[] result = new string[names.Count];
            for (int i = 0; i < names.Count; i++)
            {
                result[i] = names[i];
            }
            return result;
        }

        public string[] GetEffectsNames()
        {
            List<string> names = effects.GetNames();

            string[] result = new string[names.Count];
            for (int i = 0; i < names.Count; i++)
            {
                result[i] = names[i];
            }
            return result;
        }

        public abstract void AddEffects(GOAPWorldState state);

        public abstract ActionState PerformAction(GOAPAgent<GameObjectRef> agent, GOAPWorldState worldState);

        public abstract bool EnterAction(GOAPAgent<GameObjectRef> agent);

        public abstract bool IsInRange(GOAPAgent<GameObjectRef> agent);

        public virtual bool CanPerformAction(GOAPAgent<GameObjectRef> agent, GOAPWorldState worldState)
        {
            return worldState.CheckState(preconditions);
        }
    }
}
