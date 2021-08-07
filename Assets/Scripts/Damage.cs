using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public float pushback;

    public Damage(int dmg, float pushback) {
        damage = dmg;
        this.pushback = pushback;
    }
}
