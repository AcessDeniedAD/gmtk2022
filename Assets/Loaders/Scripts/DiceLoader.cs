using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DiceLoader", order = 1)]
public class DiceLoader : ScriptableObject
{
    public Material WhiteFace;
    public Material DarkBlueFace;
    public Material PurpleFace;
    public Material LightBlueFace;
    public Material OrangeFace;
    public Material PinkFace;
    public Material RedFace;

    public GameObject RollingDice;
}
