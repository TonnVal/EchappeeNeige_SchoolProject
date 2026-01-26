namespace Components.StateMachine.TimerStateMachine.Scripts
{
    public class TimerBlueState : TimerBaseState
    {
        public override void EnterState(TimerStateManager timer)
        {
            timer._scoreMultiplier = 25;
            GameEventService.OnTimerBlueUpdated?.Invoke(true);
        }

        public override void UpdateState(TimerStateManager timer)
        {
            if (timer._realTime > 120f)
            {
                GameEventService.OnTimerBlueUpdated?.Invoke(false);
                timer.SwitchState(timer.blueTimer);
            }
        }
    }
}