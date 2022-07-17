using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DiceLoader", order = 1)]
public class DiceLoader : ScriptableObject
{
    public Material WhiteFace;
    public Material BlueFace;
    public Material PurpleFace;
    public Material OrangeFace;
    public Material YellowFace;
    public Material GreenFace;
    public Material RedFace;

    public GameObject RollingDice;
}
