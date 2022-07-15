using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLivingObject : MonoBehaviour
{
    protected LivingObjectStats GetStats()
    {
        return gameObject.GetComponent<LivingObjectStats>();
    }
}
