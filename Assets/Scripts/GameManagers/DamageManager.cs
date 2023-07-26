using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float baseDamage;

    public int damage(float damageMultiplier)
    {
        float damage;

        damage = baseDamage * damageMultiplier;
        Debug.Log(damageMultiplier);
        return (int)damage;
    }
}
