namespace Components.StateMachine
{
    // Abstract class is like a blue-print.
    // Any state method that uses State must follow the next pattern.
    public abstract class State
    {
        protected readonly StateMachine StateMachine;

        // Constructor gives access to StateMahine and allow to change state throught StateMachine script. 
        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
