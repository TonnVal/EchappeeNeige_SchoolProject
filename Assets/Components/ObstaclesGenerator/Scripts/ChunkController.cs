using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;

    // Give access to _endAnchor reference for other scripts.
    // Other scripts can't modify the reference.
    public Transform EndAnchor => _endAnchor;

    // Return true if anchor position is inferior to 0.
    public bool IsBehind => _endAnchor.position.z <= 0;
}
