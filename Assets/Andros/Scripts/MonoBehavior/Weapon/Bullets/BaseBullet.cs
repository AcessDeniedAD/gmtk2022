using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBullet : MonoBehaviour
{
    [HideInInspector]
    public LivingObjectStats BulletOriginLivingObjectStats;

    [TagSelector]
    public List<string> TagsWhoBulletCanCollideWithLivingObjects = new List<string> { };

    public UnityAction<LivingObjectStats> BulletCollideWithLivingObjectHandler;
    public float LifeTime;
    public float Speed;
    public int BaseDamage;

    public virtual void CollisionWithLivingObject(Collider2D collision)
    {
        if (TagsWhoBulletCanCollideWithLivingObjects.Contains(collision.transform.tag))
        {
            BulletCollideWithLivingObjectHandler?.Invoke(collision.gameObject.GetComponent<LivingObjectStats>());
        }
    }
    protected virtual int CalculDamageValue(LivingObjectStats targetedLivingObjectStats)
    {
        var damage = (int)(BaseDamage * (BulletOriginLivingObjectStats.PowerRatio - targetedLivingObjectStats.DefenseRatio));
        if (damage < 0)
            damage = 0;
        return -damage;
    }
}
