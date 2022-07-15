using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseEnemy : BaseLivingObject
{
    public float BulletSpeed;
    public int FireRate;
    public GameObject Bullet;
    [TagSelector]
    public List<string> ShootablesObject;

    protected PlayerManager playerManager;
    [Inject]
    void Init(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }
}
