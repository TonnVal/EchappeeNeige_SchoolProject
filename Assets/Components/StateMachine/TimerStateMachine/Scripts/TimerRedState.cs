namespace Components.StateMachine.TimerStateMachine.Scripts
{
    public class TimerRedState : TimerBaseState
    {
        public override void EnterState(TimerStateManager timer)
        {
            timer._scoreMultiplier = 50;
            GameEventService.OnTimerRedUpdated?.Invoke(true);
        }


        public override void UpdateState(TimerStateManager timer)
        {
            if (timer._constantTimer > 180f)
            {
                GameEventService.OnTimerRedUpdated?.Invoke(false);
                timer.SwitchState(timer.blueTimer);
            }
        }
    }
}