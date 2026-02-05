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
        [SerializeField] private float _updateColorChunkTimerInterval = 60f;
        [SerializeField] private int _maxColorSwapCount = 3;
        [SerializeField] private List<ScriptableObject> _currentSlope;

        [Header("Collectible Parameters")]
        [SerializeField] private ScriptableObject _scoreCollectiblePrefab;
        [SerializeField] private float _scoreCollectibleSpawnChance;
        [SerializeField] private ScriptableObject _shieldCollectiblePrefab;
        [SerializeField] private float _shieldCollectibleSpawnChance;
        [SerializeField] private ScriptableObject _speedCollectiblePrefab;
        [SerializeField] private float _speedCollectibleSpawnChance;
        [SerializeField] private ScriptableObject _snowFloodDownCollectiblePrefab;
        [SerializeField] private float _snowFloodDownSpawnChance;

        // Reminder : getters allow reading but not modifying the value.
        public float UpdateColorChunkTimerInterval => _updateColorChunkTimerInterval;
        public int MaxColorSwapCount => _maxColorSwapCount;
        public List<ScriptableObject> CurrentSlope => _currentSlope;

        public ScriptableObject ScoreCollectiblePrefab => _scoreCollectiblePrefab;
        public ScriptableObject ShieldCollectiblePrefab => _shieldCollectiblePrefab;
        public ScriptableObject SpeedCollectiblePrefab => _speedCollectiblePrefab;
        public ScriptableObject SnowFloodDownPrefab => _snowFloodDownCollectiblePrefab;

        public float ScoreCollectibleSpawnChance => _scoreCollectibleSpawnChance;
        public float ShieldCollectibleSpawnChance => _shieldCollectibleSpawnChance;
        public float SpeedCollectibleSpawnChance => _speedCollectibleSpawnChance;
        public float SnowFloodDownSpawnChance => _snowFloodDownSpawnChance;

        // Is possible to modify dynamically the value thanks to a method with parameters in the scriptable object.
    }
}