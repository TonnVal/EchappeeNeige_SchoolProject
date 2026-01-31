using System;
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

    [Header("Slides Controller")]
    [SerializeField] private Transform[] _currentLane;
    [SerializeField] private float _slideDuration = 0.5f;
    [SerializeField] private float _crouchDuration = 0.75f;

    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerCollisionController _playerCollisionController;

    [Header("Debug")]
    [SerializeField] private bool _isSliding;
    [SerializeField] private int _currentLaneIndex = 1;
    [SerializeField] private bool _isCrouching;
    [SerializeField] private bool _isSlowingDown;

    private Coroutine _slideCoroutine;
    private Coroutine _crouchCoroutine;

    private const string CROUCH_PARAMETER = "IsCrouching";
    private const string SLOW_DOWN_PARAMETER = "IsSlowDown";

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
            if (_isCrouching)
            {
                return;
            }

            Debug.Log("Time to Crouch.");
            _crouchCoroutine = StartCoroutine(Coroutine_Crouch());
        }

        if (_actionSlowDown.action.IsPressed())
        {
            _isSlowingDown = true;
            _animator.SetBool(SLOW_DOWN_PARAMETER, true);
            GameEventService.OnPlayerBrake(true);
            
        }
        else
        {
            _isSlowingDown = false;
            _animator.SetBool(SLOW_DOWN_PARAMETER, false);
            GameEventService.OnPlayerBrake(false);
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
            // As many movements, two (or three) components are needed : distance and velocity (and speed).

            // _slideTimer increase as time flies (= velocity).
            // Set a variable to normalize time with a value between 0 and 1 to avoid potential errors.
            // Set the target position with the x position of the coroutine's argument -> "target" (= distance).
            _slideTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(_slideTimer / _slideDuration);
            var targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

            // Movement between two positions in a defined time.
            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);

            // Wait for the next frame.
            yield return null;
        }

        _isSliding = false;
    }

        private IEnumerator Coroutine_Crouch()
    {
        _isCrouching = true;
        _animator.SetBool(CROUCH_PARAMETER, true);
        _playerCollisionController.OnPlayerCrouch(true);
        
        var _crouchTimer = 0f;

        while (_crouchTimer < _crouchDuration)
        {
            _crouchTimer += Time.deltaTime;
            yield return null;
        }

        _playerCollisionController.OnPlayerCrouch(false);
        _animator.SetBool(CROUCH_PARAMETER, false);
        _isCrouching = false;
    }
}
