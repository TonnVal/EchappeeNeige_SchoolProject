using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [Header("Sphere Collider Parameters")]
    [SerializeField] private Vector3 _sphereStandCenter;
    [SerializeField] private float _sphereStandRadius;

    [SerializeField] private Vector3 _sphereCrouchStandCenter;
    [SerializeField] private float _sphereCrouchStandRadius;

    [Header("Debug")]
    [SerializeField] private bool _isHit;
    [SerializeField] private Vector3 _currentSphereCenter;
    [SerializeField] private float _currentSphereRadius;

    private readonly Collider[] _hitResults = new Collider[1];

    private void Start()
    {
        _currentSphereCenter = _sphereStandCenter;
        _currentSphereRadius = _sphereStandRadius;
    }

    private void Update()
    {
        var _hitCount = Physics.OverlapSphereNonAlloc(transform.position + _currentSphereCenter, _currentSphereRadius, _hitResults);

        if (_hitCount > 0 && !_isHit)
        {
            if (_hitResults[0].transform.CompareTag("Collectible"))
            {
                GameEventService.OnCollectiblePicked?.Invoke();
                Destroy(_hitResults[0].gameObject);
            }
            else
            {
                GameEventService.OnCollision?.Invoke();
            }

            _isHit = true;
        }
        // Reset is hit flag when no collision is detected.
        else if (_hitCount == 0)
        {
            _isHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + _currentSphereCenter, _currentSphereRadius);
    }

    public void OnPlayerCrouch(bool crouch)
    {
        if (crouch)
        {
            _currentSphereCenter = _sphereCrouchStandCenter;
            _currentSphereRadius = _sphereCrouchStandRadius;
        }
        else
        {
            _currentSphereCenter = _sphereStandCenter;
            _currentSphereRadius = _sphereStandRadius;
        }
    }
}
