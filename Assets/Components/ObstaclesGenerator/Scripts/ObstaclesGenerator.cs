using System;
using System.Collections.Generic;
using Components.Data;
using Components.SODB;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [Header("Parameters")] 
    [SerializeField] private float _translationSpeed;
    [SerializeField] private int _activeChunksCount = 2;
    [SerializeField] private int _behindChunksCount = 1;

    [Header("Prefabs")]
    // Give access to game objects with ChunkController component.
    [SerializeField] ChunkController[] _chunkPrefabs;

    // Readonly initialize just once at the game start.
    private List<ChunkController> _activeChunks = new List<ChunkController>();
    private ChunkController LastChunk => _activeChunks[_activeChunks.Count - 1];

    private bool _enabled = false;
    private bool _isSlow = false;

    private void Start()
    {
        var parameters = ScriptableObjectDataBase.GetByName("MainLevelParameters");
        HandleSpeedUpdated(PersistentData.CurrentSpeed);
        
        AddBaseChunk();
        GameEventService.OnGameState += HandleGameState;
        GameEventService.OnSpeedUpdated += HandleSpeedUpdated;
        GameEventService.OnPlayerBrake += HandleSlowDown;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameState -= HandleGameState;
        GameEventService.OnSpeedUpdated -= HandleSpeedUpdated;
        GameEventService.OnPlayerBrake -= HandleSlowDown;
    }

    private void HandleSlowDown(bool slowDown)
    {   
        if (slowDown && !_isSlow)
        {
            _translationSpeed = _translationSpeed / 2;
            _isSlow = true;
        }
        else if (!slowDown && _isSlow)
        {
            _translationSpeed = _translationSpeed * 2;
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

    private void AddChunk(Vector3 position)
    {
        ChunkController chunk = Instantiate(_chunkPrefabs[0], transform);
        chunk.transform.position = position;
        // Add chunk instantiation to _activeChunks list.
        _activeChunks.Add(chunk);
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
}
