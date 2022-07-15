using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : BaseBullet
{

    private float timer = 0;
    [TagSelector]
    public List<string> TagsWhoBulletsCanCollide = new List<string> { };
    private void Start()
    {
        BulletCollideWithLivingObjectHandler += BulletTouchLivingObject;
    }
    void BulletTouchLivingObject(LivingObjectStats targetedLivingObjectStats)
    {
        var damage = CalculDamageValue(targetedLivingObjectStats);
        targetedLivingObjectStats.AddDamage(damage);
    }

    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > LifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CollisionWithLivingObject(col);
        if (TagsWhoBulletsCanCollide.Contains(col.transform.tag))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        BulletCollideWithLivingObjectHandler -= BulletTouchLivingObject;
    }

}
