using Unity.VisualScripting;

namespace Components.StateMachine
{

    public class StateMachine
    {
        // This getter only allow StateMachine to modify CurrentState.
        public State CurrentState { get; private set; }

        // Call this methode (StateMachine.ChangeState) to change game state.
        // You need to inform the new [State](StateMachine).
        public void ChangeState(State newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update() => CurrentState?.Update();
    }
}
