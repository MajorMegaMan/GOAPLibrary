using System;
using System.Collections.Generic;

namespace GOAP
{
    public class StateMachine
    {
        public delegate void StateMachineFunc();
        StateMachineFunc m_state;

        List<StateMachineFunc> m_FuncList = new List<StateMachineFunc>();

        public StateMachine()
        {
            m_state = () => { };
        }

        // 0 for plan, 1 for move, 2 for perform
        public void SetState(int index)
        {
            index = ClampInt(index, 0, m_FuncList.Count - 1);
            
            m_state = m_FuncList[index];
        }

        public void CallState()
        {
            m_state();
        }

        public void AddStates(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                m_FuncList.Add(() => { });
            }
        }

        public void SetStateFunc(int index, StateMachineFunc func)
        {
            index = ClampInt(index, 0, m_FuncList.Count - 1);
            m_FuncList[index] = func;
        }

        int ClampInt(int value, int min, int max)
        {
            if(value < min)
            {
                return min;
            }
            else if(value > max)
            {
                return max;
            }
            return value;
        }
    }
}
