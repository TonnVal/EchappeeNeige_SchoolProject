namespace Components.StateMachine.SlopeStateMachine.Scripts
{
    public abstract class SlopeBaseState
    {
        public abstract void EnterState(SlopeStateManager slope);
        public abstract void UpdateState(SlopeStateManager slope);
        public abstract void ExitState(SlopeStateManager slope);
    }
}
