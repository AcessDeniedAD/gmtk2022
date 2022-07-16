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

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMovement.PlayerDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _playerMovement.PlayerDirection = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        _playerMovement.Dash();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _playerMovement.Jump();
    }

}
