using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsIntoPlayer : BaseLivingObject
{
    [HideInInspector]
    public List<GameObject> EquipedWeapons;

    [HideInInspector]
    public GameObject CurrentWeapon;
    [HideInInspector]
    public GameObject PreviousWeapon;
    private void Awake()
    {
        EquipedWeapons = new List<GameObject>();
    }

    public void ChangeWeapon(int id)
    {
        for (int i = 0; i < EquipedWeapons.Count; i++)
        {
            if(id == i)
            {
                PreviousWeapon = CurrentWeapon;
                CurrentWeapon = EquipedWeapons[i];
                CurrentWeapon.transform.rotation = PreviousWeapon.transform.rotation;
                EquipedWeapons[i].SetActive(true);
                continue;
            }
            EquipedWeapons[i].SetActive(false);
        }
    }
}
