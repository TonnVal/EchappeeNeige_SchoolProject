using System.Collections;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [Header("Sphere Collider Parameters")]
    [SerializeField] private Vector3 _cubeStandCenter;
    [SerializeField] private Vector3 _cubeStandRadius;
    [SerializeField] private Vector3 _cubeCrouchStandCenter;
    [SerializeField] private Vector3 _cubeCrouchStandRadius;

    [Header("Shield")]
    [SerializeField] private bool _shieldActivaded = false;
    [SerializeField] private float _shieldDuration = 10f;
    [SerializeField] private GameObject _shieldVisual;

    [Header("Debug")]
    [SerializeField] private bool _isHit;
    [SerializeField] private Vector3 _currentCubeCenter;
    [SerializeField] private Vector3 _currentCubeRadius;
    [SerializeField] private AudioSource _collectibleClip;
    [SerializeField] private AudioSource _obstacleClip;

    private readonly Collider[] _hitResults = new Collider[1];

    private void Start()
    {
        _currentCubeCenter = _cubeStandCenter;
        _currentCubeRadius = _cubeStandRadius;
    }

    private void Update()
    {
        var _hitCount = Physics.OverlapBoxNonAlloc(transform.position + _currentCubeCenter, _currentCubeRadius/2, _hitResults);

        if (_hitCount > 0 && !_isHit)
        {


            if (_hitResults[0].transform.CompareTag("ScoreCollectible"))
            {
                GameEventService.OnScoreCollectiblePicked?.Invoke();
                _collectibleClip.Play();
                Destroy(_hitResults[0].gameObject);
            }
            else if (_hitResults[0].transform.CompareTag("ShieldCollectible"))
            {
                _collectibleClip.Play();
                Destroy(_hitResults[0].gameObject);
                StartCoroutine(Coroutine_HandleShield());
            }
            else if (_hitResults[0].transform.CompareTag("SpeedCollectible"))
            {
                GameEventService.OnSpeedCollectiblePicked?.Invoke();
                _collectibleClip.Play();
                Destroy(_hitResults[0].gameObject);
            }
            else if (_hitResults[0].transform.CompareTag("SnowFloodDownCollectible"))
            {
                GameEventService.OnSnowFloodDownCollectiblePicked?.Invoke();
                _collectibleClip.Play();
                Destroy(_hitResults[0].gameObject);
            }
            else
            {
                if (_shieldActivaded)
                {
                    return;
                }

                GameEventService.OnCollision?.Invoke();
                _obstacleClip.Play();
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
        Gizmos.DrawWireCube(transform.position + _currentCubeCenter, _currentCubeRadius);
    }

    public void OnPlayerCrouch(bool crouch)
    {
            if (crouch)
            {
                _currentCubeCenter = _cubeCrouchStandCenter;
                _currentCubeRadius = _cubeCrouchStandRadius;
            }
            else
            {
                _currentCubeCenter = _cubeStandCenter;
                _currentCubeRadius = _cubeStandRadius;
            }
    }

    private IEnumerator Coroutine_HandleShield()
    {
        _shieldActivaded = true;
        _shieldVisual.SetActive(true);

        yield return new WaitForSeconds(_shieldDuration);
        
        _shieldActivaded = false;
        _shieldVisual.SetActive(false);
        yield return null;
    }
}
