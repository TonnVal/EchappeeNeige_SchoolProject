using System.Collections;
using UnityEngine;

namespace Components.StateMachine.TimerStateMachine.Scripts
{
    public abstract class TimerBaseState
    {
        public abstract void EnterState(TimerStateManager timer);
        public abstract void UpdateState(TimerStateManager timer);
    }
}