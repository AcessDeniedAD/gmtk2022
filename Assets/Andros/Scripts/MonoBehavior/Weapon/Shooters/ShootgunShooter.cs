using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootgunShooter : BaseShooter
{
    public GameObject Bullet;
    public int BulletNumber;
    public float ConeSize;
    public float FireRate;
    private bool canShot;
    private void Start()
    {
        canShot = true;

    }
    void OnEnable()
    {
        LivingObjectWhoHaveThisShooter = gameObject.transform.root.gameObject;
        StartCoroutine(AfterShoot());
    }
    public override void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canShot)
            {
                WeaponFeedbacks.PlayFBShoot();
                for (int i = 0; i < BulletNumber; i++)
                {
                    var yspread = Random.Range(-ConeSize, ConeSize);
                    var go = InstantiantBulletAndAssignOriginStats(Bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, yspread), LivingObjectWhoHaveThisShooter.GetComponent<LivingObjectStats>());
                }
                LivingObjectWhoHaveThisShooter.GetComponent<PlayerMovements>().SetRecoil(Vector3.Normalize(transform.rotation * -Vector3.right), 3, 5f);
                StartCoroutine(AfterShoot());
            }
            else
            {
                Debug.Log("wait for reload");
            }

        }
    }
    IEnumerator AfterShoot()
    {
        canShot = false;
        yield return new WaitForSeconds(FireRate);
        canShot = true;
    }
    void Collide()
    {

    }
}
