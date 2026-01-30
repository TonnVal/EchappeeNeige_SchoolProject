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
        [SerializeField] private float _snowFlood = 0;
        [SerializeField] private float _updateColorChunkTimerInterval = 60f;
        [SerializeField] private List<float> _speed;
        [SerializeField] private List<int> _updatePointMultiplicator;

        [Header("Chunk Colors")]
        [SerializeField] private List<Material> _chunkMaterial;
        [SerializeField] private int _maxColorSwapCount = 3;

        // Reminder : it's a getter which allow reading but not modifying the value.
        public float SnowFlood => _snowFlood;
        public float UpdateColorChunkTimerInterval => _updateColorChunkTimerInterval;
        public List<float> Speed => _speed;
        public List<int> UpdatePointScred => _updatePointMultiplicator;


        public List<Material> ChunkMaterial => _chunkMaterial;
        public int MaxColorSwapCount => _maxColorSwapCount;

        // Is possible to modify dynamically the value thanks to a method with parameters in the scriptable object.
    }
}