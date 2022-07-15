using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingObjectEvents : BaseManager
{
    public UnityAction<Args> DeathHandler;
    public UnityAction<Args> DamageHandler;
    private readonly GameManager gameManager;
    public LivingObjectEvents(GameManager gameManager)
    {
        Debug.Log("PlayerEvents");
        this.gameManager = gameManager;
        EventsManager.StartListening(nameof(DeathHandler), Death);

    }
    public class LivingObjectDeathArgs : Args
    {
        public GameObject Go;
    }
    public class LivingObjectDamageArgs : Args
    {
        public GameObject Go;
        public int Damage;
    }
    private void Death(Args args)
    {
        if (args.GetType() != typeof(LivingObjectEvents.LivingObjectDeathArgs))
            throw new Exception("argument must be a LivingObjectDeathArgs");
        LivingObjectEvents.LivingObjectDeathArgs _args = ((LivingObjectEvents.LivingObjectDeathArgs)args);
        _args.Go.SetActive(false);
    }
}
