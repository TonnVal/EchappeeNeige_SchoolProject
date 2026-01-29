using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        private float _currentSnowFlood;
        private int _snowFloodImpactValue = 10;

        private float _currentTime = 0f;
        private float _currentScore = 0f;
        private int _currentMultiplior = 5;
        
        public GameState(StateMachine stateMachine) : base(stateMachine) { }
        
        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnScoreIncrease += ScoreIncreasing;
            GameEventService.OnCollision += HandleCollision;
        }

        public override void Update()
        {
            GameEventService.OnScoreIncrease?.Invoke(_currentScore);
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
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
        }
    }
}
