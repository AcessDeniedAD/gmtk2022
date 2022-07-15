using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatlingShooter : BaseShooter
{
    public GameObject Bullet;
    private float pressedValue;
    private float timer;
    public float fireRate = 0.2f;
    private bool canFirstBulletShoot = true;
    private float duration = 0;

    void OnEnable()
    {
     
    }
    // Update is called once per frame
    void OnDisable()
    {
      
    }
     public override void Shoot(InputAction.CallbackContext context)
    {
        if (context.started && canFirstBulletShoot && Ammo > 0)
        {
            StartCoroutine(FireCoolDown());
        }
        if (context.performed)
        {
            pressedValue = context.ReadValue<float>();
           
        }
        else
        {
            pressedValue = 0;
        }

    }
    private void Update()
    {
        if(pressedValue > 0 && Ammo > 0)
        {
            timer += Time.deltaTime;
            duration += Time.deltaTime;
            if(timer >= fireRate)
            {
                timer = 0;
                Ammo--;
                var yspread = Random.Range(-5, 5);
                WeaponFeedbacks.PlayFBShoot();
                InstantiantBulletAndAssignOriginStats(Bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, yspread), LivingObjectWhoHaveThisShooter.GetComponent<LivingObjectStats>());
            }
            return;
        }
        timer = 0;
    }
    IEnumerator FireCoolDown()
    {
        var timer = 0f;
        canFirstBulletShoot = false;
        Ammo--;
        var yspread = Random.Range(-5, 5);
        WeaponFeedbacks.PlayFBShoot();
        InstantiantBulletAndAssignOriginStats(Bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, yspread), LivingObjectWhoHaveThisShooter.GetComponent<LivingObjectStats>());
        while (timer < fireRate)
        {
            timer += Time.deltaTime;
            yield return 0;
        }
        canFirstBulletShoot = true;
    }
}
