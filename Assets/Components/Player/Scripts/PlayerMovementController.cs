using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Input Movement")]
    [SerializeField] private InputActionReference _actionMoveLeft;
    [SerializeField] private InputActionReference _actionMoveRight;
    [SerializeField] private InputActionReference _actionCrouch;
    [SerializeField] private InputActionReference _actionSlowDown;

    [Header("Slide Controller")]
    [SerializeField] private Transform[] _currentLane;
    [SerializeField] private float _slideDuration = 0.5f;

    [Header("Debug")]
    [SerializeField] private bool _isSliding;
    [SerializeField] private int _currentLaneIndex = 1;

    private Coroutine _slideCoroutine;

    private void OnEnable()
    {
        _actionMoveLeft.action.Enable();
        _actionMoveRight.action.Enable();
        _actionCrouch.action.Enable();
        _actionSlowDown.action.Enable();
    }

    private void Update()
    {
        if (_actionMoveLeft.action.WasPerformedThisFrame())
        {
            if (_isSliding)
            {
                StopCoroutine(_slideCoroutine);
            }
            
            if (_currentLaneIndex == 0)
            {
                return;
            }

            _currentLaneIndex--;
            _slideCoroutine = StartCoroutine(Coroutine_Slide(_currentLane[_currentLaneIndex]));
        }

        if (_actionMoveRight.action.WasPerformedThisFrame())
        {
            if (_isSliding)
            {
                StopCoroutine(_slideCoroutine);
            }
            
            if (_currentLaneIndex == _currentLane.Length - 1)
            {
                return;
            }

            _currentLaneIndex++;
            _slideCoroutine = StartCoroutine(Coroutine_Slide(_currentLane[_currentLaneIndex]));
        }

        if (_actionCrouch.action.WasPerformedThisFrame())
        {
            return;
        }
    }

    private IEnumerator Coroutine_Slide(Transform target)
    {
        _isSliding = true;
        
        var _slideTimer = 0f;
        
        while (_slideTimer < _slideDuration)
        {

            _slideTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(_slideTimer / _slideDuration);
            var targetPosition = new Vector3 (target.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);
        
            yield return null;
        }
        
        _isSliding = false;
    }
}
