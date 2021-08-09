using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public float pushback;
    public GameObject source;

    public Damage(int dmg, float pushback, GameObject src) {
        damage = dmg;
        this.pushback = pushback;
        source = src;
    }
}
