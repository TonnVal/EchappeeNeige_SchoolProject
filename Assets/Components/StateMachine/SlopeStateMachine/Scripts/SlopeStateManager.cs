using Assets.Components.StateMachine.SlopeStateMachine.Scripts;
using UnityEngine;

namespace Components.StateMachine.SlopeStateMachine.Scripts
{
    public class SlopeStateManager : MonoBehaviour
    {
        [Header("Slope State Machine")]
        SlopeBaseState currentState;
        SlopeGreenState greenState = new SlopeGreenState();
        SlopeBlueState blueState = new SlopeBlueState();
        SlopeRedState redState = new SlopeRedState();
        SlopeBlackState blackState = new SlopeBlackState();

        private void Start()
        {
            currentState = greenState;

            currentState.EnterState(this);
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        private void SwitchState(SlopeBaseState slope)
        {
            currentState = slope;
            slope.EnterState(this);
        }
    }
}
