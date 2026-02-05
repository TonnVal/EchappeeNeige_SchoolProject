using Components.Data;
using Components.SODB;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [Header("Chunk Parameters")]
    private Transform _chunk;
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private MeshRenderer _meshRendererLeftPath;
    [SerializeField] private MeshRenderer _meshRandererRightPath;

    [Header("Collectible Parameters")]
    [SerializeField] private List<Transform> _spawnPointScoreCollectible;
    [SerializeField] private List<Transform> _spawnPointShieldCollectible;
    [SerializeField] private List<Transform> _spawnPointSpeedCollectible;
    [SerializeField] private List<Transform> _spawnPointSnowFloodDownCollectible;

    // Give access to _endAnchor reference for other scripts.
    // Other scripts can't modify the reference.
    public Transform EndAnchor => _endAnchor;

    // Return true if anchor position is inferior to 0.
    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        GameEventService.OnChunkChangeColor += HandleChunkColorUpdated;
        var levelParameters = ScriptableObjectDataBase.Get<SOLevelParameters>("MainLevelParameters");

        _chunk = gameObject.transform;
        
        var scoreSpawnLocation = _spawnPointScoreCollectible[Random.Range(0, _spawnPointScoreCollectible.Count)].transform.position;
        var shieldSpawnLocation = _spawnPointShieldCollectible[Random.Range(0, _spawnPointShieldCollectible.Count)].transform.position;
        var speedSpawnLocation = _spawnPointSpeedCollectible[Random.Range(0, _spawnPointSpeedCollectible.Count)].transform.position;
        var snowFloodDownSpawnLocation = _spawnPointSnowFloodDownCollectible[Random.Range(0, _spawnPointSnowFloodDownCollectible.Count)].transform.position;

        if (levelParameters.ScoreCollectibleSpawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= levelParameters.ScoreCollectibleSpawnChance;

            if (randomSpawnChance)
            {
                SOCollectible collectiblePrefab = (SOCollectible)levelParameters.ScoreCollectiblePrefab;
                GameObject ScoreCollectible = CollectibleGenerator.Generator(collectiblePrefab);

                ScoreCollectible.transform.position = scoreSpawnLocation;
                ScoreCollectible.transform.SetParent(_chunk);
            }
        }

        if (levelParameters.ScoreCollectibleSpawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= levelParameters.ShieldCollectibleSpawnChance;

            if (randomSpawnChance)
            {
                SOCollectible collectiblePrefab = (SOCollectible)levelParameters.ShieldCollectiblePrefab;
                GameObject ShieldCollectible = CollectibleGenerator.Generator(collectiblePrefab);

                ShieldCollectible.transform.position = shieldSpawnLocation;
                ShieldCollectible.transform.SetParent(_chunk);
            }
        }

        if (levelParameters.ScoreCollectibleSpawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= levelParameters.SpeedCollectibleSpawnChance;

            if (randomSpawnChance)
            {
                SOCollectible collectiblePrefab = (SOCollectible)levelParameters.SpeedCollectiblePrefab;
                GameObject SpeedCollectible = CollectibleGenerator.Generator(collectiblePrefab);

                SpeedCollectible.transform.position = speedSpawnLocation;
                SpeedCollectible.transform.SetParent(_chunk);
            }
        }

        if (levelParameters.SnowFloodDownSpawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= levelParameters.SnowFloodDownSpawnChance;

            if (randomSpawnChance)
            {
                SOCollectible collectiblePrefab = (SOCollectible)levelParameters.SnowFloodDownPrefab;
                GameObject SnowFloodDownCollectible = CollectibleGenerator.Generator(collectiblePrefab);

                SnowFloodDownCollectible.transform.position = snowFloodDownSpawnLocation;
                SnowFloodDownCollectible.transform.SetParent(_chunk);
            }
        }
    }

    private void OnDestroy()
    {
        GameEventService.OnChunkChangeColor -= HandleChunkColorUpdated;
    }

    private void HandleChunkColorUpdated(Material newMaterial)
    {
        if (!newMaterial)
        {
            return;
        }
        
        _meshRendererLeftPath.material = newMaterial;
        _meshRandererRightPath.material = newMaterial;
    }
}
