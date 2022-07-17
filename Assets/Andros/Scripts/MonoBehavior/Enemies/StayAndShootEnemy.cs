using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StayAndShootEnemy : BaseEnemy
{
    private List<GameObject> players;
    private float timer = 0;
    //private void Start()
    //{
    //    players = playerManager.Player;
    //}
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= FireRate)
    //    {
    //        timer = 0;
    //        ShootIfPlayerIsOnVision();
    //    }
    //}
    //private void ShootIfPlayerIsOnVision()
    //{
    //    foreach (var player in players)
    //    {
    //        if (!player.activeInHierarchy)
    //            continue;
    //        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position);
    //        if (ShootablesObject.Contains(hit.transform.tag))
    //        {
    //            Debug.DrawLine(transform.position, player.transform.position, Color.red);
    //            var go = Instantiate(Bullet, transform);
    //            go.GetComponent<BaseBullet>().BulletOriginLivingObjectStats = gameObject.GetComponent<LivingObjectStats>();
    //            var dir = player.transform.position - transform.position;
    //            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //            go.GetComponent<BaseBullet>().Speed = BulletSpeed;
    //            go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //        }

    //    }
    //}
}
