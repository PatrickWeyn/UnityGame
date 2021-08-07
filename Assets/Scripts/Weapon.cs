using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int mindam;
    public int maxdam;
    public float pushback;

    private Animator anim;
    public float cooldown;
    private float lastswing;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.tag == "Fighter")
            collision.gameObject.SendMessage("ReceiveDamage", new Damage(Random.Range(mindam, maxdam), pushback, this.transform.parent.gameObject));
    }

    private void Swing() {
        if (Time.time - lastswing > cooldown) { 
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
