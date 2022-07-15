using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseLivingObject
{
    [HideInInspector]
    public Vector2 CursorMovement = Vector2.zero;

    private int CurrentWeaponId = 0;
    private WeaponsIntoPlayer weaponsIntoPlayer;
    private PlayerMovements playerMovements;

    private void Start()
    {
        weaponsIntoPlayer = gameObject.GetComponent<WeaponsIntoPlayer>();
        playerMovements = gameObject.GetComponent<PlayerMovements>();
    }
    public void OnMoveCursor(InputAction.CallbackContext context)
    {
        CursorMovement = context.ReadValue<Vector2>();
    }
    public void OnChangeWeaponLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CurrentWeaponId--;
            if (CurrentWeaponId < 0)
            {
                CurrentWeaponId = weaponsIntoPlayer.EquipedWeapons.Count - 1;
            }
            weaponsIntoPlayer.ChangeWeapon(CurrentWeaponId);
        }
    }
    public void OnChangeWeaponRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CurrentWeaponId++;
            if (CurrentWeaponId > weaponsIntoPlayer.EquipedWeapons.Count - 1)
            {
                CurrentWeaponId = 0;
            }
            weaponsIntoPlayer.ChangeWeapon(CurrentWeaponId);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
            weaponsIntoPlayer.CurrentWeapon.GetComponent<WeaponController>().Shoot(context);
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        playerMovements.Dash(context, Vector3.one * CursorMovement, 10);
    }
}
