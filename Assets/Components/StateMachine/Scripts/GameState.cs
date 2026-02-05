using Components.Data;
using Components.SODB;
using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters) { }

        private float _chunkTimer;
        private int _slopeSwapCount = 0;
        
        private SOSlopeParameters slopeParameters;
        private int _scoreMultiplicator;
        private int _obstacleCollisionValue;
        private float _snowFloodIncreaseTimer;

        private float _currentScore = 0;
        private int _currentSnowFlood = 0;
        private float _currentSnowFloodTimer = 0;
        private int _maxSnowFlood = 100;
            
        
        public override void Enter()
        {
            slopeParameters = ScriptableObjectDataBase.Get<SOSlopeParameters>("GreenSlope");
            _scoreMultiplicator = slopeParameters.ScoreMultiplicator;
            _obstacleCollisionValue = slopeParameters.ObstacleCollisionValue;
            _snowFloodIncreaseTimer = slopeParameters.SnowFloodIncreaseTimer;
            
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnPlayerBrake += SnowFloodTimerDivisor;

            GameEventService.OnScoreCollectiblePicked += HandleScoreCollectible;
            GameEventService.OnSnowFloodDownCollectiblePicked += HandleSnowFloodDown;
        }

        public override void Update()
        {
            _currentScore += Time.deltaTime * _scoreMultiplicator;
            GameEventService.OnScoreIncrease?.Invoke(_currentScore);

            _currentSnowFloodTimer += Time.deltaTime;
            if (_currentSnowFloodTimer >= _snowFloodIncreaseTimer)
            {
                _currentSnowFlood ++;
                _currentSnowFloodTimer = 0;
            }
            GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);
            
            if (_slopeSwapCount >= LevelParameters.MaxColorSwapCount)
            {
                return;
            }
            
            _chunkTimer += Time.deltaTime;
            if (_chunkTimer >= LevelParameters.UpdateColorChunkTimerInterval)
            {
                slopeParameters = (SOSlopeParameters)LevelParameters.CurrentSlope[_slopeSwapCount];

                _scoreMultiplicator = slopeParameters.ScoreMultiplicator;
                _obstacleCollisionValue = slopeParameters.ObstacleCollisionValue;
                _snowFloodIncreaseTimer = slopeParameters.SnowFloodIncreaseTimer;
                
                GameEventService.OnChunkChangeColor?.Invoke(slopeParameters.ChunkMaterial);
                GameEventService.OnSpeedUpdated?.Invoke(slopeParameters.Speed);
                GameEventService.OnFieldOfViewUpdated?.Invoke(slopeParameters.UpdateFOV);

                _slopeSwapCount++;
                _chunkTimer = 0;
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting Game State");
            GameEventService.OnScoreCollectiblePicked -= HandleScoreCollectible;
            GameEventService.OnSnowFloodDownCollectiblePicked -= HandleSnowFloodDown;

            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnPlayerBrake -= SnowFloodTimerDivisor;
            GameEventService.OnGameState?.Invoke(false);

        }

        private void HandleCollision()
        {
            _currentSnowFlood += _obstacleCollisionValue;

            if (_currentSnowFlood >= _maxSnowFlood)
            {
                GameEventService.OnSnowFloodUpdated?.Invoke(_currentSnowFlood);
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }

        }

        private void SnowFloodTimerDivisor(bool slowDown)
        {
            if (slowDown)
            {
                _snowFloodIncreaseTimer = slopeParameters.SnowFloodIncreaseTimer / 2;
            }
            else
            {
                _snowFloodIncreaseTimer = slopeParameters.SnowFloodIncreaseTimer;
            }
        }

        private void HandleScoreCollectible()
        {
            _currentScore += slopeParameters.ScoreBonus;
        }

        private void HandleSnowFloodDown()
        {
            _currentSnowFlood -= 10;

            if(_currentSnowFlood < 0 )
            {
                _currentSnowFlood = 0;
            }
        }
    }
}
