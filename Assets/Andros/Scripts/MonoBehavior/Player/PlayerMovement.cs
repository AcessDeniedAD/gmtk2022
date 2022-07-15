using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 0.5f;

    [SerializeField]
    private float _dashDistance = 3f;

    public void MoveAlongDirection(Vector2 playerDirection)
    {
        transform.position += new Vector3 (playerDirection.x, 0, playerDirection.y) * _playerSpeed * Time.deltaTime;
    }

    public void Dash(Vector2 playerDirection)
    {
        transform.position += new Vector3(playerDirection.x, 0, playerDirection.y) * _dashDistance;
    }

    public void Jump()
    {
    }
}
