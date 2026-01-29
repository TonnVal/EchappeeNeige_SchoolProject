using UnityEngine;

namespace Components.StateMachine
{
    public class GameOverState : State
    {
        public GameOverState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            GameEventService.OnGameOver?.Invoke(true);
        }
        
        public override void Update()
        {
            //noop.
        }

        public override void Exit()
        {
            GameEventService.OnGameOver?.Invoke(false);
        }

    }
}
