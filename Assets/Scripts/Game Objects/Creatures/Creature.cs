using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Creature : MonoBehaviour {
    public float speedx;
    public float speedy;

    //Ability Scores
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;

    public int health;
    public int maxhealth;
    public float pushrecovery;


    private Vector3 pushdirection;
    private RaycastHit2D hit;

    protected virtual void ReceiveDamage(Damage dmg) {
        health -= dmg.damage;
        pushdirection = (transform.position - dmg.source.transform.position).normalized * dmg.pushback;
        GameManager.app.ftm.ShowMessage("-" + dmg.damage.ToString(), "dmg", transform.position);
        if (health <= 0) Death(dmg);
    }

    protected void MoveCreature(Vector3 input) {
        //Determine the difference between our current position and the new position
        Vector3 movedelta = new Vector3(input.x * speedx, input.y * speedy, 0);

        ChangeSpriteDirection(movedelta);

        //Add push vector, if any
        movedelta += pushdirection;
        pushdirection = Vector3.Lerp(pushdirection, Vector3.zero, pushrecovery);

        //Check if something is blocking the way in vertical direction
        hit = Physics2D.BoxCast(transform.position, gameObject.GetComponent<BoxCollider2D>().size, 0, new Vector2(0, movedelta.y), Mathf.Abs(movedelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null || hit.collider.GetType() == typeof(CircleCollider2D)) {
            transform.Translate(0, movedelta.y * Time.deltaTime, 0);
        }
        //Check if something is blocking the way in vertical direction

        hit = Physics2D.BoxCast(transform.position, gameObject.GetComponent<BoxCollider2D>().size, 0, new Vector2(movedelta.x, 0), Mathf.Abs(movedelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null || hit.collider.GetType() == typeof(CircleCollider2D)) {
            transform.Translate(movedelta.x * Time.deltaTime, 0, 0);
        }
    }

    protected abstract void ChangeSpriteDirection(Vector3 dir);

    protected virtual void Death(Damage dmg) {
        Destroy(this.gameObject);
    }

    public int GetScore(string ability) {
        return ability switch {
            "STR" => STR,
            "DEX" => DEX,
            "CON" => CON,
            "INT" => INT,
            "WIS" => WIS,
            "CHA" => CHA,
            _ => throw new Exception("Creature.GetScore() received incorrect string \"" + ability + "\""),
        };
    }

}
