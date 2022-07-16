using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public BaseShooter Shooter;
    public BaseWeaponFeedbacks WeaponFeedbacks;
    private Vector3 center;
    private PlayerController playerController;
    private Vector2 lastDir;

    public void Init()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
        center = transform.parent.position;
    }

    void FixedUpdate()
    {
        //var dir = playerController.CursorMovement;
        //if(dir != Vector2.zero)
        //{
        //    lastDir = dir;
        //}

        var angle = Mathf.Atan2(lastDir.y, lastDir.x ) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        Shooter.Shoot(context);
    }
}
