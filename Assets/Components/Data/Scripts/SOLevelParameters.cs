using System.Collections.Generic;
using UnityEngine;

namespace Components.Data
{

    // A scriptable object is not a Behavior script and herit from a ScriptableObject instance.
    // Every scriptable object is an asset, so we must inform Unity by the following brackets.
    // In the project window, we can now create a new asset "Data".
    [CreateAssetMenu(menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [Header("Main Parameters")]
        [SerializeField] private List<float> _speed;
        [SerializeField] private float _updateColorChunkTimerInterval = 60f;
        [SerializeField] private List<float> _updateFOV;

        [Header("Score Parameters")]
        [SerializeField] private List<int> _updatePointMultiplicator;

        [Header("Snow Flood Parameters")]
        [SerializeField] private float _snowFlood = 0;
        [SerializeField] private List<int> _snowFloodImpact;
        [SerializeField] private float _snowFloodMainTimer = 8f;
        [SerializeField] private List<float> _snowFloodTimerIncrease;

        [Header("Chunk Colors")]
        [SerializeField] private List<Material> _chunkMaterial;
        [SerializeField] private Material _chunkStartColor;
        [SerializeField] private int _maxColorSwapCount = 3;

        [Header("Collectible Parameters")]
        [SerializeField] private List<float> _scoreBonus;

        // Reminder : it's a getter which allow reading but not modifying the value.
        public List<float> Speed => _speed;
        public float UpdateColorChunkTimerInterval => _updateColorChunkTimerInterval;
        public List<float> UpdateFOV => _updateFOV;

        public List<int> UpdatePointScred => _updatePointMultiplicator;

        public float SnowFlood => _snowFlood;
        public List<int> SnowFloodImpact => _snowFloodImpact;
        public float SnowFloodMainTimer => _snowFloodMainTimer;
        public List<float> SnowFloodTimerIncrease => _snowFloodTimerIncrease;

        public List<Material> ChunkMaterial => _chunkMaterial;
        public int MaxColorSwapCount => _maxColorSwapCount;
        public Material ChunkStartColor => _chunkStartColor;

        public List<float> ScoreBonus => _scoreBonus;

        // Is possible to modify dynamically the value thanks to a method with parameters in the scriptable object.
    }
}