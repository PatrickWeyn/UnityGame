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
        GameManager.app.UI.SendMessage("UpdateWeapon");
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Fighter" && collision.GetType() == typeof(BoxCollider2D))
            collision.gameObject.SendMessage("ReceiveDamage", new Damage(Random.Range(weapon.mindmg, weapon.maxdmg), weapon.pushback, this.transform.parent.gameObject));
    }

    private void Swing() {
        if (Time.time - lastswing > weapon.cooldown) { 
        lastswing = Time.time;
        anim.SetTrigger("Swing");
        }
        else {
            Debug.Log("Your swing is on cooldown!");
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && gameObject.transform.parent.name == "Player") {
            Swing();
        }
    }
}
