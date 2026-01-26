using UnityEngine;

namespace Components.StateMachine.TimerStateMachine.Scripts
{
    public class TimerGreenState : TimerBaseState
    {
        public override void EnterState(TimerStateManager timer)
        {
            timer._scoreMultiplier = 10;
            GameEventService.OnTimerGreenUpdated?.Invoke(true);
        }
        
        public override void UpdateState(TimerStateManager timer)
        {
            if(timer._realTime > 60f)
            {
                GameEventService.OnTimerGreenUpdated?.Invoke(false);
                timer.SwitchState(timer.blueTimer);
            }
        }
    }
}