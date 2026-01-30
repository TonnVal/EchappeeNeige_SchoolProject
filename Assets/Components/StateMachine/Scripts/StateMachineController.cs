using Components.SODB;
using UnityEngine;

namespace Components.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        
        // Create an instance for StateMachine.
        private void Start()
        {
            var parameters = ScriptableObjectDataBase.GetByName("MainLevelParameters");
            
            _stateMachine = new StateMachine();
            // Change here to give another start to state machine.
            var initialState = new CountdownState(_stateMachine, parameters);

            _stateMachine.ChangeState(initialState);
        }

        // Need this Update to updating StateMachineController.
        public void Update() => _stateMachine.Update();
    }
}