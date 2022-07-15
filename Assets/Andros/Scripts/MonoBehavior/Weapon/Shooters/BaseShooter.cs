using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseShooter : MonoBehaviour
{
    public int Ammo = 10;
    public int MaxAmmo = 100;
    protected BaseWeaponFeedbacks WeaponFeedbacks;
    [HideInInspector]
    public GameObject LivingObjectWhoHaveThisShooter;
    public void Init()
    {
        LivingObjectWhoHaveThisShooter = gameObject.transform.root.gameObject;
        WeaponFeedbacks = gameObject.transform.parent.GetComponent<WeaponController>().WeaponFeedbacks;
    }

    public virtual void Shoot(InputAction.CallbackContext context) 
    {
        Debug.Log("UnOvirreded virtual shoot");
    }
    public virtual GameObject InstantiantBulletAndAssignOriginStats(GameObject original, Vector3 pos, Quaternion rot, LivingObjectStats livingObjectStats)
    {
        var go = Instantiate(original, pos, rot);
        go.GetComponent<BaseBullet>().BulletOriginLivingObjectStats = livingObjectStats;
        return go;
    }
}
