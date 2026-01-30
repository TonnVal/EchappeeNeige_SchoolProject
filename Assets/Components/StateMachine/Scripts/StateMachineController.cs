using Components.Data;
using UnityEngine;

namespace Components.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField] private SOLevelParameters _levelParameters;
        private StateMachine _stateMachine;
        
        // Create an instance for StateMachine.
        private void Start()
        {
            _stateMachine = new StateMachine();
            // Change here to give another start to state machine.
            var initialState = new CountdownState(_stateMachine, _levelParameters);

            _stateMachine.ChangeState(initialState);
        }

        // Need this Update to updating StateMachineController.
        public void Update() => _stateMachine.Update();
    }
}