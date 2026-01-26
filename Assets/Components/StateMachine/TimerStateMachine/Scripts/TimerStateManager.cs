using UnityEngine;

namespace Components.StateMachine.TimerStateMachine.Scripts
{

    public class TimerStateManager : MonoBehaviour
    {
        [Header("Timer State Machine")]
        TimerBaseState currentState;
        public TimerGreenState greenTimer = new TimerGreenState();
        public TimerBlueState blueTimer = new TimerBlueState();
        public TimerRedState redTimer = new TimerRedState();
        public TimerBlackState blackTimer = new TimerBlackState();

        [Header("State Parameters")]
        [SerializeField] public int _scoreMultiplier;
        
        [Header("Debug")]
        [SerializeField] public float _realTime = 0f;
        [SerializeField] public float _scoreByTime = 0f;

        private void Start()
        {   
            currentState = greenTimer;

            currentState.EnterState(this);
            GameEventService.HandleTimerForScore += Scoring;
        }
        private void OnDestroy()
        {
            GameEventService.HandleTimerForScore -= Scoring;
        }

        private void Update()
        {
            _realTime += Time.deltaTime;
            Scoring(_scoreByTime);
            GameEventService.HandleTimerForScore?.Invoke(_scoreByTime);
            currentState.UpdateState(this);
        }

        public void SwitchState(TimerBaseState timer)
        {
            currentState = timer;
            timer.EnterState(this);
        }

        private void Scoring(float score)
        {
            score = _scoreByTime;
            _scoreByTime += Time.deltaTime * _scoreMultiplier;
        }
    }
}
