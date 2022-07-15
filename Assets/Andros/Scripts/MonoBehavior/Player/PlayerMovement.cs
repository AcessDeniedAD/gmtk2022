using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 0.5f;

    [SerializeField]
    private float _dashSpeed = 3f;

    [SerializeField]
    private float _dashTime = 3f;

    [SerializeField]
    private float _dashCoolDown = 2f;

    private bool _isDashing = false;

    private bool _canDash = true;

    public void MoveAlongDirection(Vector2 playerDirection)
    {
        transform.position += new Vector3 (playerDirection.x, 0, playerDirection.y) * _playerSpeed * Time.deltaTime * Time.timeScale;
    }

    public void Dash(Vector2 playerDirection)
    {
        if(_canDash)
        {
            StartCoroutine(DashCoroutine(playerDirection));
        }
    }

    private IEnumerator DashCoroutine(Vector2 playerDirection)
    {
        _canDash = false;
        var _remainingDashTime = _dashTime;
        while(_remainingDashTime > 0)
        {
            transform.position += new Vector3(playerDirection.x, 0, playerDirection.y) * _dashSpeed * Time.fixedDeltaTime * Time.timeScale;
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

    public void Jump()
    {
    }
}
