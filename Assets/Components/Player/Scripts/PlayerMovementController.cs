using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Input Movement")]
    [SerializeField] private InputActionReference _actionMoveLeft;
    [SerializeField] private InputActionReference _actionMoveRight;
    [SerializeField] private InputActionReference _actionCrouch;
    [SerializeField] private InputActionReference _actionSlowDown;

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
            this.transform.position = new Vector3(10, 0, 0);
        }

        if (_actionMoveRight.action.WasPerformedThisFrame())
        {
            this.transform.position = new Vector3(-10, 0, 0);
        }

        if (_actionCrouch.action.WasPerformedThisFrame())
        { 
            this.transform.position = new Vector3(0, -5, 0); 
        }
    }
}
