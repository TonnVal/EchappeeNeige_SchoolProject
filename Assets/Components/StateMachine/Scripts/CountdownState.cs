namespace Components.StateMachine
{
    public class CountdownState : State
    {
        private float _countdownTimer;

        // Constructor that respect the State condition.
        // Base define the base-constructor.
        public CountdownState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            _countdownTimer = 3;
        }
        public override void Update()
        {
            _countdownTimer -= UnityEngine.Time.deltaTime;
            if (_countdownTimer > 0)
            {
                return;
            }

            // Ask StateMachine to change state.
            // stateMachine became GameState.
            StateMachine.ChangeState(new GameState(StateMachine));
        }

        public override void Exit()
        {
            // noop
        }
    }
}
