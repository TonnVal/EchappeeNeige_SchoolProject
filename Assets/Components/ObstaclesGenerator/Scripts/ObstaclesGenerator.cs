using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [Header("Parameters")] 
    [SerializeField] private float _translationSpeed;
    [SerializeField] private int _activeChunksCount = 2;
    [SerializeField] private int _behindChunksCount = 1;
    [SerializeField] private bool _preventSameChunkGeneration = true;

    [Header("Boost Parameters")]
    [SerializeField] private bool _boost = false;
    [SerializeField] private float _boostDuration = 10f;
    [SerializeField] private float _boostValue = 1.5f;

    [Header("Prefabs")]
    // Give access to game objects with ChunkController component.
    [SerializeField] ChunkController[] _chunkPrefabs;

    // Readonly initialize just once at the game start.
    private List<ChunkController> _activeChunks = new List<ChunkController>();
    private ChunkController LastChunk => _activeChunks[_activeChunks.Count - 1];

    private int _lastChunkIndex;
    private bool _enabled = false;
    private bool _isSlow = false;

    private void Start()
    {
        AddBaseChunk();
        GameEventService.OnGameState += HandleGameState;
        GameEventService.OnSpeedUpdated += HandleSpeedUpdated;
        GameEventService.OnPlayerBrake += HandleSlowDown;
        GameEventService.OnSpeedCollectiblePicked += HandleBoost;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameState -= HandleGameState;
        GameEventService.OnSpeedUpdated -= HandleSpeedUpdated;
        GameEventService.OnPlayerBrake -= HandleSlowDown;
        GameEventService.OnSpeedCollectiblePicked -= HandleBoost;
    }

    private void HandleSlowDown(bool slowDown)
    {   
        if (slowDown && !_isSlow)
        {
            _translationSpeed /= 2;
            _isSlow = true;
        }
        else if (!slowDown && _isSlow)
        {
            _translationSpeed *= 2;
            _isSlow = false;
        }
    }

    private void HandleSpeedUpdated(float newSpeed)
    {
        if (newSpeed == 0)
        {
            return;
        }
        _translationSpeed = newSpeed;
    }

    private void HandleGameState(bool enterState)
    {
        _enabled = enterState;
    }

    private void Update()
    {
        if (!_enabled)
        {
            return;
        }
        
        // For each chunk in the _activeChunks list, a translation is opered.
        foreach (ChunkController chunk in _activeChunks)
        {
            chunk.transform.Translate(Vector3.back * (_translationSpeed * Time.deltaTime));
        }

        UpdateChunks();
    }

    private void AddBaseChunk()
    {
        for (int i = 0; i < _activeChunksCount; i++)
        {
            if (i == 0)
            {
                AddChunk(Vector3.zero);
                // Continue to next iteration.
                continue;
            }

            AddChunk(LastChunk.EndAnchor.position);
        }
    }

    private ChunkController AddChunk(Vector3 position)
    {
        var newChunkIndex = Random.Range(0, _chunkPrefabs.Length);

        if (_preventSameChunkGeneration)
        {
            for (int i = 0; i < 10; i ++)
            {
                if (newChunkIndex == _lastChunkIndex)
                {
                    newChunkIndex = Random.Range(0, _chunkPrefabs.Length);
                }
            }
            _lastChunkIndex = newChunkIndex;
        }
        
        ChunkController chunk = Instantiate(_chunkPrefabs[newChunkIndex], transform);
        chunk.transform.position = position;
        // Add chunk instantiation to _activeChunks list.
        _activeChunks.Add(chunk);

        return chunk;
    }

    private void UpdateChunks()
    {
        List<ChunkController> behindChunks = new();

        // Add chunk to behindChunks list if IsBehind is true.
        foreach (var chunk in _activeChunks)
        {
            if (chunk.IsBehind)
            {
                behindChunks.Add(chunk);
            }
        }

        // Verify the number of chunks behind the camera.
        // Remove the farther chunk to the list and destroy it.
        if (behindChunks.Count > _behindChunksCount)
        {
            int chunkToDeleteCount = behindChunks.Count -_behindChunksCount;
            for (int i = 0; i < chunkToDeleteCount; i++)
            {
                var chunkToDelete = behindChunks[i];
                _activeChunks.Remove(chunkToDelete);
                Destroy(chunkToDelete.gameObject);
            }
        }

        // Add chunks till validate the variable _activeChunksCount.
        int missingChunkCount = _activeChunksCount - _activeChunks.Count;
        for (int i = 0; i < missingChunkCount; i++)
        {
            AddChunk(LastChunk.EndAnchor.position);
        }
    }

    private void HandleBoost()
    {
        if (!_boost)
        {
            StartCoroutine(Coroutine_HandleBoost());
        }
    }

    private IEnumerator Coroutine_HandleBoost()
    {
        _boost = true;
        _translationSpeed *= _boostValue;

        yield return new WaitForSeconds(_boostDuration);

        _translationSpeed /= _boostValue;
        _boost = false;
        yield return null;
    }
}
