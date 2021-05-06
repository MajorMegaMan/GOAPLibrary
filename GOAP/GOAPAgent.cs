using System.Collections.Generic;

namespace GOAP
{
    public class GOAPAgent<GameObjectRef>
    {
        GameObjectRef agentGameObject;

        StateMachine m_stateMachine = new StateMachine();

        // GOAP
        GOAPWorldState worldState = null;
        GOAPWorldState m_combinedWorldState;
        GOAPWorldState m_selfishWorldState = null;
        Queue<GOAPAgentAction<GameObjectRef>> m_plan = new Queue<GOAPAgentAction<GameObjectRef>>();
        GOAPBehaviour<GameObjectRef> m_behaviour;

        GOAPAgentAction<GameObjectRef> m_currentAction;

        public delegate MovementFlag MoveToFunc();
        MoveToFunc m_moveDelegate = () => { return MovementFlag.COMPLETE; };

        public delegate void Func();
        Func m_enterNavigation = () => { };
        Func m_exitNavigation = () => { };

        public enum State
        {
            PLANNING,
            MOVE_TO,
            PERFORM_ACTION
        }

        public enum MovementFlag
        {
            PARTIAL,
            COMPLETE,
            UNABLE
        }

        public GOAPAgent(GameObjectRef agentGameObject)
        {
            this.agentGameObject = agentGameObject;

            // initialise states
            m_stateMachine.AddStates(3);
            m_stateMachine.SetStateFunc(0, DecideState);
            m_stateMachine.SetStateFunc(1, MoveTo);
            m_stateMachine.SetStateFunc(2, PerformAction);

            m_stateMachine.SetState(0);
        }

        public void SetEnterNavigationFunc(Func enterNavigation)
        {
            m_enterNavigation = enterNavigation;
        }

        public void SetExitNavigationFunc(Func exitNavigation)
        {
            m_exitNavigation = exitNavigation;
        }

        public void SetMoveToDelegate(MoveToFunc moveDelegate)
        {
            m_moveDelegate = moveDelegate;
        }

        public void SetSelfishWorldState()
        {
            m_selfishWorldState = m_behaviour.GetSelfishNeeds();
        }

        public GameObjectRef GetAgentObject()
        {
            return agentGameObject;
        }

        // this returns the actual selfishworldstate
        public GOAPWorldState GetSelfishNeeds()
        {
            return m_selfishWorldState;
        }

        // Update is called once per frame
        public void Update()
        {
            m_behaviour.Update(this, m_selfishWorldState);
            m_stateMachine.CallState();
        }

        public bool SetWorldState(GOAPWorldState worldState)
        {
            this.worldState = worldState;
            if (m_selfishWorldState != null)
            {
                m_combinedWorldState = GOAPWorldState.CombineWithReferences(worldState, m_selfishWorldState);
                return true;
            }
            return false;
        }

        // returns a new worldstate from the cobined world and this agents selfish needs
        public GOAPWorldState GetWorldState()
        {
            return new GOAPWorldState(m_combinedWorldState);
        }

        public void SetBehaviour(GOAPBehaviour<GameObjectRef> behaviour)
        {
            m_behaviour = behaviour;
            //m_agentWorldState = m_behaviour.GetSelfishNeeds();
            // new behaviour will most likely not contain the actions remaining in the queue
            // so just simply clear them all and then the agent will find a new plan.
            m_plan.Clear();

            m_selfishWorldState = m_behaviour.GetSelfishNeeds();

            if (worldState != null)
            {
                m_combinedWorldState = GOAPWorldState.CombineWithReferences(worldState, m_selfishWorldState);
            }
        }

        public void SetState(State desiredState)
        {
            m_stateMachine.SetState((int)desiredState);
        }

        public void FindPlan()
        {
            // Get GOAPplan
            // need to find goal
            m_plan = m_behaviour.CalcPlan(m_combinedWorldState);
            m_stateMachine.SetState(0);
        }

        public Queue<GOAPAgentAction<GameObjectRef>> GetPlan()
        {
            return new Queue<GOAPAgentAction<GameObjectRef>>(m_plan);
        }

        public List<GOAPAction> GetUsableActions()
        {
            return m_behaviour.GetUsableActions(m_combinedWorldState);
        }

        public GOAPAction GetAction()
        {
            return m_currentAction;
        }

        void DecideState()
        {
            if (m_plan.Count > 0)
            {
                // plan was found
                m_currentAction = m_plan.Dequeue();

                // Check if the action target was assigned correctly
                if (!m_currentAction.EnterAction(this))
                {
                    // Plan is garbage now find a new one
                    FindPlan();
                    return;
                }
                // if in range of action
                if (m_currentAction.IsInRange(this))
                {
                    // state = perform
                    SetState(State.PERFORM_ACTION);
                    // also intiate the first perform frame
                    PerformAction();
                }
                else
                {
                    // else 
                    // state = moveto
                    StartNavigation();
                    // also intiate the first moveTo frame
                    MoveTo();
                }
            }
            else
            {
                // there is no plan
                // find a new one
                FindPlan();
                m_currentAction = null;
            }
        }

        void StartNavigation()
        {
            // user defined delegate will "initialise movement variables"
            m_enterNavigation();

            // then set the state to move
            SetState(State.MOVE_TO);
        }

        void ProcessNavigationFlag(MovementFlag flag)
        {
            // user defined delegate will "deinitialise movement var" or "close movement var"
            switch (flag)
            {
                case MovementFlag.PARTIAL:
                    {
                        // agent has not moved but is still able to perform the action
                        // agent can continue moving
                        break;
                    }
                case MovementFlag.COMPLETE:
                    {
                        // agent has moved and will need to check if in range of action
                        if (m_currentAction.IsInRange(this))
                        {
                            // if action is in range exit movement
                            m_exitNavigation();
                            SetState(State.PERFORM_ACTION);
                        }
                        break;
                    }
                case MovementFlag.UNABLE:
                    {
                        // target action is no longer accessable
                        m_exitNavigation();
                        FindPlan();
                        break;
                    }
            }
        }

        void MoveTo()
        {
            if (!m_currentAction.CanPerformAction(this, m_combinedWorldState))
            {
                // cannot perform current action, so stop moving to it and find a new plan
                ProcessNavigationFlag(MovementFlag.UNABLE);
                return;
            }

            // User defined Delegate for movement function will go here
            MovementFlag flag = m_moveDelegate();
            ProcessNavigationFlag(flag);
        }

        void PerformAction()
        {
            // Check result of performing action
            switch (m_currentAction.PerformAction(this, m_combinedWorldState))
            {
                case GOAPAgentAction<GameObjectRef>.ActionState.completed:
                    {
                        // action was completed progress to the next action
                        m_currentAction.AddEffects(m_combinedWorldState);
                        m_stateMachine.SetState(0);
                        break;
                    }
                case GOAPAgentAction<GameObjectRef>.ActionState.performing:
                    {
                        // still performing the action. Nothing needs to be done?
                        // maybe still need to check if in range
                        break;
                    }
                case GOAPAgentAction<GameObjectRef>.ActionState.interrupt:
                    {
                        // action was interrupted and as a result was not completed therefore a new plan may be needed
                        FindPlan();
                        break;
                    }
            }
        }
    }
}
