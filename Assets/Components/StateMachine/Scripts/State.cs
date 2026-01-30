using Components.Data;

namespace Components.StateMachine
{
    // Abstract class is like a blue-print.
    // Any state method that uses State must follow the next pattern.
    public abstract class State
    {
        protected readonly StateMachine StateMachine;
        protected readonly SOLevelParameters LevelParameters;

        // Constructor gives access to StateMachine and allow to change state throught StateMachine script. 
        protected State(StateMachine stateMachine, SOLevelParameters levelParameters)
        {
            StateMachine = stateMachine;
            LevelParameters = levelParameters;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
