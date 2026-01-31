using System;
using System.Collections;
using Components.Data;
using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        private float _currentSnowFlood;
        private float _snowFloodMax = 100f;
        private int _snowFloodImpactValue = 5;
        private float _snowFloodTimer;
        private float _snowFloodTimerMax;

        private float _currentTime = 0f;
        private float _currentScore = 0f;
        private int _currentMultiplicator;

        private float _chunkTimer;
        private int _colorSwapCount;
        
        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters) { }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnScoreIncrease += ScoreIncreasing;
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnPlayerBrake += SnowFloodTimerDivisor;

            _currentSnowFlood = 0f;

            _currentMultiplicator = 10;

            _chunkTimer = 0;
            _snowFloodTimer = 0;
        }

        public override void Update()
        {
            GameEventService.OnScoreIncrease?.Invoke(_currentScore);
            GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);
            
            _snowFloodTimer += Time.deltaTime;
            if (_snowFloodTimer >= _snowFloodTimerMax)
            {
                _currentSnowFlood++;
                _snowFloodTimer = 0;
            }

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

                var speed = LevelParameters.Speed[_colorSwapCount];
                GameEventService.OnSpeedUpdated?.Invoke(speed);

                var multiplicator = LevelParameters.UpdatePointScred[_colorSwapCount];
                GameEventService.OnScoreMultiplicatorUpdated?.Invoke(multiplicator);

                var impactValue = LevelParameters.SnowFloodImpact[_colorSwapCount];

                var snowFloodTimerIncrease = LevelParameters.SnowFloodTimerIncrease[_colorSwapCount];

                _currentMultiplicator = multiplicator;
                _snowFloodImpactValue = impactValue;
                _colorSwapCount++;
                _chunkTimer = 0;
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting Game State");
            _currentSnowFlood = 0;
            GameEventService.OnScoreIncrease -= ScoreIncreasing;
            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnPlayerBrake -= SnowFloodTimerDivisor;
            GameEventService.OnGameState?.Invoke(false);
        }
        
        private void ScoreIncreasing(float obj)
        {
            _currentTime += Time.deltaTime;
            _currentScore = _currentTime * _currentMultiplicator;
        }

        private void HandleCollision()
        {
            _currentSnowFlood += _snowFloodImpactValue;
            Debug.Log($"New SnowFlood value = {_currentSnowFlood}");
            GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);

            if (_currentSnowFlood >= _snowFloodMax)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }
        }

        private void SnowFloodTimerDivisor(bool slowDown)
        {
            if (slowDown)
            {
                _snowFloodTimerMax = (LevelParameters.SnowFloodMainTimer - LevelParameters.SnowFloodTimerIncrease[_colorSwapCount]) / 2;
            }
            else
            {
                _snowFloodTimerMax = LevelParameters.SnowFloodMainTimer - LevelParameters.SnowFloodTimerIncrease[_colorSwapCount];
            }
        }
    }
}
