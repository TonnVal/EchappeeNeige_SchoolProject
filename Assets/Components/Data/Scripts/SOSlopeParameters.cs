using System.Collections.Generic;
using UnityEngine;

namespace Components.Data
{

    [CreateAssetMenu(menuName = "Data/SlopeParameters")]
    public class SOSlopeParameters : ScriptableObject
    {
        [Header("Slope Main Parameters")]
        [SerializeField] private Material _chunkMaterial;
        [SerializeField] private float _speed;
        [SerializeField] private float _updateFOV;

        [Header("Score Parameters")]
        [SerializeField] private int _scoreMultiplicator;

        [Header("Snow Flood Parameters")]
        [SerializeField] private int _obstacleCollisionValue;
        [SerializeField] private float _snowFloodIncreaseTimer = 8f;

        public Material ChunkMaterial => _chunkMaterial;
        public float Speed => _speed;
        public float UpdateFOV => _updateFOV;
        public int ScoreMultiplicator => _scoreMultiplicator;
        public int ObstacleCollisionValue => _obstacleCollisionValue;
        public float SnowFloodIncreaseTimer => _snowFloodIncreaseTimer;
    }
}
