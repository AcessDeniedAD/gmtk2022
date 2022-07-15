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
        _mainPlayerInput = new MainPlayerInput();
        _mainPlayerInput.Enable();
        if(_playerMovement == null)
        {
            _playerMovement = gameObject.GetComponent<PlayerMovement>();
        }

        _mainPlayerInput.Player.Move.performed += OnMove;
        _mainPlayerInput.Player.Move.canceled += OnMove;
        _mainPlayerInput.Player.Dash.started += OnDash;
        _mainPlayerInput.Player.Jump.started += OnJump;

    }

    private void OnDestroy()
    {
        _mainPlayerInput.Player.Move.performed -= OnMove;
        _mainPlayerInput.Player.Move.canceled -= OnMove;
        _mainPlayerInput.Player.Dash.performed -= OnDash;
        _mainPlayerInput.Player.Dash.performed -= OnJump;

        _mainPlayerInput.Disable(); 
    }

    private void FixedUpdate()
    {
        _playerMovement.MoveAlongDirection(PlayerDirection);
        Vector3 movement = Vector3.right * PlayerDirection.x + Vector3.forward * PlayerDirection.y;
        if(movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement, transform.up);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            PlayerDirection = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        _playerMovement.Dash(PlayerDirection);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Should jump");
    }

}
