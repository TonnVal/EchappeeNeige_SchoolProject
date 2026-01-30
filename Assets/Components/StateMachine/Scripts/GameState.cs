using Components.Data;
using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        private float _currentSnowFlood;
        private int _snowFloodImpactValue = 10;
        private float _snowFloodMax = 100f;

        private float _currentTime = 0f;
        private float _currentScore = 0f;
        private int _currentMultiplior = 5;
        
        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters) { }
        
        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnScoreIncrease += ScoreIncreasing;
            GameEventService.OnCollision += HandleCollision;

            _currentSnowFlood = LevelParameters.SnowFlood;
        }

        public override void Update()
        {
            GameEventService.OnScoreIncrease?.Invoke(_currentScore);
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnScoreIncrease -= ScoreIncreasing;
            GameEventService.OnCollision -= HandleCollision;
        }
        
        private void ScoreIncreasing(float obj)
        {
            _currentTime += Time.deltaTime;
            _currentScore = _currentTime * _currentMultiplior;
        }

        private void HandleCollision()
        {
            _currentSnowFlood += _snowFloodImpactValue;
            Debug.Log("New SnowFlood value = " + _currentSnowFlood);
            GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);

            if (_currentSnowFlood == _snowFloodMax)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }
        }
    }
}
