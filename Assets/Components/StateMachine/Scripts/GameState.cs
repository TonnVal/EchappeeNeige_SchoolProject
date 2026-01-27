using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        [Header("Play Game Parameters")]
        private float _currentTime = 0f;
        private float _currentScore = 0f;
        private int _currentMultiplior = 5;
        
        public GameState(StateMachine stateMachine) : base(stateMachine) { }
        
        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnScoreIncrease += ScoreIncreasing;
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
    }
}
