using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int mindam;
    public int maxdam;
    public float pushback;
    public float cooldown;
    private float lastswing;

    public void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.tag == "Fighter")
            collision.gameObject.SendMessage("ReceiveDamage", new Damage(Random.Range(mindam, maxdam), pushback, this.transform.parent.gameObject));
    }
}
