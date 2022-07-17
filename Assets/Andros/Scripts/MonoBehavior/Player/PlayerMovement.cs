using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 PlayerDirection;

    public StatesManager _statesManager;
    public PlayerManager _playerManager;
    [SerializeField]
    private float _playerSpeed = 0.5f;

    [SerializeField]
    private float _dashSpeed = 3f;

    [SerializeField]
    private float _dashTime = 3f;

    [SerializeField]
    private float _dashCoolDown = 2f;

    [SerializeField]
    private float _gravity = -9.81f;

    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private float verticalVelocity;

    [SerializeField]
    private float groundDistanceCheck = 0.1f;

    private bool _isDashing = false;

    private bool _canDash = true;

    public bool _isGrounded = false;

    public bool _hasJumped = false;

    private void Awake()
    {
        EventsManager.StartListening(nameof(StatesEvents.OnLooseIn), Loose);
    }

    private void FixedUpdate()
    {
        verticalVelocity += _gravity * Time.fixedDeltaTime;
        ChackIfGrounded();
        ApplyGravity(verticalVelocity);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 0.1f);
        Debug.DrawRay(transform.position, Vector3.down, Color.yellow);
    }

    private void Update()
    {
        Vector3 movement = Vector3.right * PlayerDirection.x + Vector3.forward * PlayerDirection.y;
        MoveAlongDirection(movement);
        AdjustOrientation(movement);
        CheckIsDead();
    }

    private void CheckIsDead()
    {
        if (transform.position.y < -1.0f && !_statesManager.IsCurrentState(new States.Loose()))
        {
            _statesManager.ChangeCurrentState(new States.Loose());
            
        }


    }

    private void Loose(Args args)
    {
        if (!_playerManager.IsDead)
        {
            _playerManager.SetIsDead();
        }
    }

    private void ChackIfGrounded()
    {
        if (CheckCollision(Vector3.down) && verticalVelocity < 0)
        {
            verticalVelocity = 0;
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private bool CheckCollision(Vector3 direction)
    {
        RaycastHit Hit;
        return Physics.BoxCast(transform.position + Vector3.up * (groundDistanceCheck+0.1f), Vector3.one * 0.1f, -Vector3.up, out Hit, Quaternion.identity, groundDistanceCheck);
            
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 0.1f);
    }

    public void ApplyGravity(float currentVelocity)
    {
        //Debug.Log(currentVelocity);
        transform.Translate(new Vector3(0, currentVelocity, 0) * Time.fixedDeltaTime);
    }

    public void MoveAlongDirection(Vector3 playerDirection)
    {
        transform.position += playerDirection * _playerSpeed * Time.deltaTime * Time.timeScale;
    }

    public void Dash()
    {
        if(_canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        _canDash = false;
        var _remainingDashTime = _dashTime;
        while(_remainingDashTime > 0)
        {
            transform.position += new Vector3(PlayerDirection.x, 0, PlayerDirection.y) * _dashSpeed * Time.fixedDeltaTime * Time.timeScale;
            _remainingDashTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        var _remainingCDTime = _dashCoolDown;
        while(_remainingCDTime > 0)
        {
            _remainingCDTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        _canDash = true;
    }

    private void AdjustOrientation(Vector3 playerDirection)
    {
        if (playerDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, transform.up);
        }
    }

    public void Jump()
    {
        if( _isGrounded)
        {
            verticalVelocity = _jumpForce;
        }
    }
}
