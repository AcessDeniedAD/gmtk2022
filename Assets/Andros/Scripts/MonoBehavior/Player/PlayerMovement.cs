using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 PlayerDirection;

    Animator _playerAnimatorController;


    public StatesManager _statesManager;
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
        if (_playerAnimatorController == null)
        {
            _playerAnimatorController = gameObject.GetComponentInChildren<Animator>();
        }
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
        Debug.Log("Camera rot = " + Camera.main.transform.rotation.y);
        float CameraAngle = Vector3.Angle(Vector3.right, Camera.main.transform.right) * Mathf.Deg2Rad;
        Vector3 movement = (Vector3.right * Mathf.Cos(CameraAngle) + Vector3.forward * Mathf.Sin(CameraAngle)) * PlayerDirection.y +
            (Vector3.right *  Mathf.Sin(CameraAngle) + Vector3.forward * - Mathf.Cos(CameraAngle))* PlayerDirection.x;
        _playerAnimatorController.SetFloat("speed", movement.magnitude);
        MoveAlongDirection(movement);
        AdjustOrientation(movement);
        checkIfCanGetCoin();
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
        Debug.Log("C'est loose enculé :D");
    }

    private void ChackIfGrounded()
    {
        if (CheckCollision(Vector3.down) && verticalVelocity < 0)
        {
            verticalVelocity = 0;
            if(!_isGrounded)
            {
                EventsManager.TriggerEvent("PlayerLand");
            }
            _isGrounded = true;
            _playerAnimatorController.SetBool("isJumping", false);
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
            _playerAnimatorController.SetBool("isJumping", true);
            EventsManager.TriggerEvent("PlayerJump");
            verticalVelocity = _jumpForce;
        }
    }

    public void checkIfCanGetCoin()
    {

        Vector3 p1 = transform.position ;
        RaycastHit[] hits = Physics.SphereCastAll(p1, 0.2f, transform.forward, 0.2f);
         foreach(RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Coin")
            {
                EventsManager.TriggerEvent("PlayerCatchCoin");
                Score.Coins++;
                if(Score.Coins % 5 ==0)
                {
                    EventsManager.TriggerEvent("CrowdCheer");
                }
                if(Score.Coins % 20 ==0)
                {
                    EventsManager.TriggerEvent("JackPotSound");
                }
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
