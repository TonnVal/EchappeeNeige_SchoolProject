using Assets.Components.StateMachine.SlopeStateMachine.Scripts;
using UnityEngine;

namespace Components.StateMachine.SlopeStateMachine.Scripts
{
    public class SlopeStateManager : MonoBehaviour
    {
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
    }
}
