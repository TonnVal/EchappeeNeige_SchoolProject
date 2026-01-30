using System;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private MeshRenderer _meshRendererLeftPath;
    [SerializeField] private MeshRenderer _meshRandererRightPath;

    // Give access to _endAnchor reference for other scripts.
    // Other scripts can't modify the reference.
    public Transform EndAnchor => _endAnchor;

    // Return true if anchor position is inferior to 0.
    public bool IsBehind => _endAnchor.position.z <= 0;

    private void Start()
    {
        GameEventService.OnChunkChangeColor += HandleChunkColorUpdated;
        HandleChunkColorUpdated(PersistentData.CurrentChunkMaterial);
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
