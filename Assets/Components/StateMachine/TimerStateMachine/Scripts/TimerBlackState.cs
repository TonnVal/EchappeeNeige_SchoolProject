namespace Components.StateMachine.TimerStateMachine.Scripts
{
    public class TimerBlackState : TimerBaseState
    {
        public override void EnterState(TimerStateManager timer)
        {
            timer._scoreMultiplier = 100;
            GameEventService.OnTimerBlackUpdated?.Invoke(true);
        }

        public override void UpdateState(TimerStateManager timer)
        {
            
        }
    }
}