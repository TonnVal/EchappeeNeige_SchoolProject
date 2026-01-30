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

        [Header("Score Parameters")]
        [SerializeField] private List<int> _updatePointMultiplicator;

        [Header("Snow Flood Parameters")]
        [SerializeField] private float _snowFlood = 0;
        [SerializeField] private List<int> _snowFloodImpact;
        [SerializeField] private List<float> _snowFloodTimerIncrease;

        [Header("Chunk Colors")]
        [SerializeField] private List<Material> _chunkMaterial;
        [SerializeField] private int _maxColorSwapCount = 3;

        // Reminder : it's a getter which allow reading but not modifying the value.
        public List<float> Speed => _speed;
        public float UpdateColorChunkTimerInterval => _updateColorChunkTimerInterval;

        public List<int> UpdatePointScred => _updatePointMultiplicator;

        public float SnowFlood => _snowFlood;
        public List<int> SnowFloodImpact => _snowFloodImpact;
        public List<float> SnowFloodTimerIncrease => _snowFloodTimerIncrease;

        public List<Material> ChunkMaterial => _chunkMaterial;
        public int MaxColorSwapCount => _maxColorSwapCount;

        // Is possible to modify dynamically the value thanks to a method with parameters in the scriptable object.
    }
}