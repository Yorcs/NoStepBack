using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] protected bool doesPoison, doesFreeze, doesBleed, doesStun, doesBurn = false;
    [SerializeField] protected float statusDuration = 0;
    [SerializeField] protected int statusDamage = 0;
    
    
    public bool GetPoisoned()
    {
        return doesPoison;
    }

    public bool GetFrozen()
    {
        return doesFreeze;
    }
    public float GetStatusDuration()
    {
        return statusDuration;
    }

    public int GetStatusDamage()
    {
        return statusDamage;
    }
}
