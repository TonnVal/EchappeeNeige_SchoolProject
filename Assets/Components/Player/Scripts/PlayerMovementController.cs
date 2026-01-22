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

    /// <summary>
    /// Handle slide input between lanes.
    /// Check the "_isSliding" condition for debugging.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator Coroutine_Slide(Transform target)
    {
        _isSliding = true;
        
        // Set to 0 a timer for sliding.
        var _slideTimer = 0f;
        
        // Loop who runs till the timer is not equal to the slide's duration.
        while (_slideTimer < _slideDuration)
        {

            // _slideTimer increase as time flies.
            // Set a variable to normalize time with a value between 0 and 1 to avoid potential errors.
            // Set the target position with the x position of the coroutine's argument -> "target".
            _slideTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(_slideTimer / _slideDuration);
            var targetPosition = new Vector3 (target.position.x, transform.position.y, transform.position.z);

            // Movement between two positions in a defined time.
            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);
        
            // Wait for the next frame.
            yield return null;
        }
        
        _isSliding = false;
    }
}
