using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : BaseManager
{
    [HideInInspector]
    public List<GameObject> Players;
    public PlayerManager()
    {
        Players = new List<GameObject>();        
    }
    public void StartPlayerInSceneWithEquipedWeapons(GameObject player, Vector3 dir, List<GameObject> weaponsPrefab)
    {
        var playerGo = GameObject.Instantiate(player);
        playerGo.GetComponent<PlayerMovements>().Direction = dir;
        Players.Add(playerGo);
        for (int i = 0; i < weaponsPrefab.Count; i++)
        {
            var weaponGo = GameObject.Instantiate(weaponsPrefab[i]);
            playerGo.GetComponent<WeaponsIntoPlayer>().EquipedWeapons.Add(weaponGo);
            weaponGo.transform.parent = playerGo.transform;
            weaponGo.transform.localPosition = Vector3.zero;
            weaponGo.GetComponent<WeaponController>().Init();
            weaponGo.GetComponent<WeaponController>().Shooter.Init();
            if (i != 0)
            {
                weaponGo.SetActive(false);
            }
            else
            {
                playerGo.GetComponent<WeaponsIntoPlayer>().CurrentWeapon = weaponGo;
            }
        }
    }
   
}
