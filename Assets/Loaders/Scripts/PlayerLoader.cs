using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerLoader", order = 1)]
public class PlayerLoader : ScriptableObject
{
    public GameObject PlayerPrefab;
    public float Speed;
}
