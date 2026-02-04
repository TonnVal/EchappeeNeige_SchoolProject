using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [Header("Chunk Paramteters")]
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private MeshRenderer _meshRendererLeftPath;
    [SerializeField] private MeshRenderer _meshRandererRightPath;

    [Header("Collectible Parameters")]
    [SerializeField] private List<GameObject> _collectiblePrefabs;
    [SerializeField] private List<Transform> _spawnPoint;
    [SerializeField, Range(0, 99)] private int _spawnChance;

    // Give access to _endAnchor reference for other scripts.
    // Other scripts can't modify the reference.
    public Transform EndAnchor => _endAnchor;

    // Return true if anchor position is inferior to 0.
    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        GameEventService.OnChunkChangeColor += HandleChunkColorUpdated;

        if (_spawnChance != 0)
        {
            bool randomSpawnChance = Random.Range(0, 100) <= _spawnChance;
            if (randomSpawnChance)
            {
                Instantiate(_collectiblePrefabs[Random.Range(0, _collectiblePrefabs.Count)], _spawnPoint[Random.Range(0, _spawnPoint.Count)]);
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
