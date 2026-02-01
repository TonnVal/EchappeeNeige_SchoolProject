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

        private float _scoreBonus;
        
        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters) { }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnScoreIncrease += ScoreIncreasing;
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnPlayerBrake += SnowFloodTimerDivisor;
            GameEventService.OnScoreCollectiblePicked += HandleCollectiblePicked;

            _currentSnowFlood = 0f;

            _currentMultiplicator = 10;

            _chunkTimer = 0;
            _snowFloodTimer = 0;

            _scoreBonus = 40;
        }

        public override void Update()
        {
            GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);
            GameEventService.OnScoreIncrease?.Invoke(_currentScore);
            
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

                var fov = LevelParameters.UpdateFOV[_colorSwapCount];
                GameEventService.OnFieldOfViewUpdated?.Invoke(fov);

                var scoreBonus = LevelParameters.ScoreBonus[_colorSwapCount];

                _currentMultiplicator = multiplicator;
                _snowFloodImpactValue = impactValue;
                _scoreBonus = scoreBonus;

                _colorSwapCount++;
                _chunkTimer = 0;
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting Game State");
            GameEventService.OnScoreIncrease -= ScoreIncreasing;
            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnPlayerBrake -= SnowFloodTimerDivisor;
            GameEventService.OnScoreCollectiblePicked -= HandleCollectiblePicked;
            GameEventService.OnGameState?.Invoke(false);
        }
        
        private void ScoreIncreasing(float currentScore)
        {
            _currentTime += Time.deltaTime;
            _currentScore = _currentTime * _currentMultiplicator;
            currentScore = _currentScore;
        }

        private void HandleCollision()
        {
            _currentSnowFlood += _snowFloodImpactValue;
            Debug.Log($"New SnowFlood value = {_currentSnowFlood}");
            GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);

            if (_currentSnowFlood >= _snowFloodMax)
            {
                _currentScore = _currentScore;
                GameEventService.OnFinalScore?.Invoke(_currentScore);
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
                _snowFloodTimerMax = (LevelParameters.SnowFloodMainTimer - LevelParameters.SnowFloodTimerIncrease[_colorSwapCount]);
            }
        }

        private void HandleCollectiblePicked()
        {
            //_currentScore += _scoreBonus;

            _currentSnowFlood -= 10;
            if (_currentSnowFlood < 0)
            {
                _currentSnowFlood = 0;
            }
        }
    }
}
