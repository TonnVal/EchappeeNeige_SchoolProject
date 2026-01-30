using Components.Data;
using System.Runtime.CompilerServices;
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

        private float _chunkTimer;
        private int _colorSwapCount;
        
        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters) { }
        
        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnScoreIncrease += ScoreIncreasing;
            GameEventService.OnCollision += HandleCollision;

            _currentSnowFlood = LevelParameters.SnowFlood;
            _chunkTimer = 0;
        }

        public override void Update()
        {
            GameEventService.OnScoreIncrease?.Invoke(_currentScore);

            if (_colorSwapCount >= LevelParameters.MaxColorSwapCount)
            {
                return;
            }
            
            _chunkTimer += Time.deltaTime;
            if (_chunkTimer > LevelParameters.UpdateColorChunkTimerInterval)
            {
                var material = LevelParameters.ChunkMaterial[_colorSwapCount];
                GameEventService.OnChunkChangeColor?.Invoke(material);
                PersistentData.CurrentChunkMaterial = material;

                _colorSwapCount++;
                _chunkTimer = 0;
            }
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
