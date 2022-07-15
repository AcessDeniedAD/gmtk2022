using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseLivingObject
{
    
    public Vector2 PlayerDirection = Vector2.zero;

    [SerializeField]
    private PlayerMovement _playerMovement;

    private MainPlayerInput _mainPlayerInput;

    private void Awake()
    {
        if(_playerMovement == null)
        {
            _playerMovement = gameObject.GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        _playerMovement.MoveAlongDirection(PlayerDirection);
        Vector3 movement = Vector3.right * PlayerDirection.x + Vector3.forward * PlayerDirection.y;
        transform.rotation = Quaternion.LookRotation(movement, transform.up);
    }

    public void Move(InputAction.CallbackContext context)
    {
        PlayerDirection = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        //_playerMovement.Dash(context, Vector3.one * CursorMovement, 10);
    }

    private float CalculateFacingAngle(Vector2 facingDirection)
    {
        var angle = Mathf.Asin((facingDirection.y)/facingDirection.magnitude);
        return angle;
    }
}
