using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBulletCollider : BaseBullet
{
    private Collider2D bulletCollider;
    private void Start()
    {
        BulletCollideWithLivingObjectHandler += BulletTouchLivingObject;
        bulletCollider = gameObject.GetComponent<Collider2D>();
        bulletCollider.enabled = false;
    }
    public void StartColliding()
    {
        bulletCollider.enabled = true;
        StartCoroutine(DisabledWithLifeTime());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CollisionWithLivingObject(col);
    }
    void BulletTouchLivingObject(LivingObjectStats targetedLivingObjectStats)
    {
        var damage = CalculDamageValue(targetedLivingObjectStats);
        targetedLivingObjectStats.AddDamage(damage);
    }
    IEnumerator DisabledWithLifeTime()
    {
        yield return new WaitForSeconds(LifeTime);
        bulletCollider.enabled = false;
    }
    public override void CollisionWithLivingObject(Collider2D collision)
    {
        if (TagsWhoBulletCanCollideWithLivingObjects.Contains(collision.transform.tag))
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, collision.transform.position);//le depart doit etre la position du shooter
            Debug.DrawLine(gameObject.transform.position, collision.transform.position);
            Debug.Log(hit.transform.name);
            BulletCollideWithLivingObjectHandler?.Invoke(collision.gameObject.GetComponent<LivingObjectStats>());
        }
    }
}
