using UnityEngine;

namespace Components.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        
        // Create an instance for StateMachine.
        private void Awake()
        {
            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine);

            _stateMachine.ChangeState(initialState);
        }

        public void Update() => _stateMachine.Update();
    }
}