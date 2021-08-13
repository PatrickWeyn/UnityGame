using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SO_weapon weapon;

    private Animator anim;
    private float lastswing;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Fighter")
            collision.gameObject.SendMessage("ReceiveDamage", new Damage(Random.Range(weapon.mindmg, weapon.maxdmg), weapon.pushback, this.transform.parent.gameObject));
    }

    public void Swing() {
        if (Time.time - lastswing > weapon.cooldown && gameObject.transform.parent.name == "Player") { 
        lastswing = Time.time;
        anim.SetTrigger("Swing");
        }
    }
}
