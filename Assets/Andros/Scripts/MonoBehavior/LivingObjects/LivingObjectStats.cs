using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObjectStats : BaseLivingObject
{
    public int Id;
    public int MaxHp;
    public int Hp;
    public int Shield;
    public float PowerRatio = 1;
    public float DefenseRatio = 0f;
    public int Speed;

    public void AddDamage(int value)
    {
        if (Hp +value > MaxHp)
        {
            Hp = MaxHp;
            return;
        }
        Hp += value;
        if(Hp <= 0)
        {
            Hp = 0;
            EventsManager.TriggerEvent(nameof(LivingObjectEvents.DeathHandler), new LivingObjectEvents.LivingObjectDeathArgs {Go = gameObject});
        }
        EventsManager.TriggerEvent(nameof(LivingObjectEvents.DamageHandler), new LivingObjectEvents.LivingObjectDamageArgs { Go = gameObject, Damage = value });
    }

    public void AddShield()
    {

    }

    public void AddPowerRatio()
    {

    }

    public void AddDefenseRatio()
    {

    }
    public void AddSpeed()
    {

    }



}
