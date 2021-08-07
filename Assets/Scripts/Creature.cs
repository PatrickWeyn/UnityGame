using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Creature : MonoBehaviour {
    public float speedx;
    public float speedy;

    public int health;

    private RaycastHit2D hit;

    protected void ReceiveDamage(Damage dmg) {
        health -= dmg.damage;
        if (health <= 0) Death(dmg);
        GameManager.app.ftm.ShowMessage("-" + dmg.damage.ToString(), "dmg", transform.position);
    }

    protected void MoveCreature(Vector3 input) {
        //Determine the difference between our current position and the new position
        Vector3 movedelta = new Vector3(input.x * speedx, input.y * speedy, 0);

        ChangeSpriteDirection(movedelta);

        //Check if something is blocking the way in vertical direction
        hit = Physics2D.BoxCast(transform.position, gameObject.GetComponent<BoxCollider2D>().size, 0, new Vector2(0, movedelta.y), Mathf.Abs(movedelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null) {
            transform.Translate(0, movedelta.y * Time.deltaTime, 0);
        }
        //Check if something is blocking the way in vertical direction

        hit = Physics2D.BoxCast(transform.position, gameObject.GetComponent<BoxCollider2D>().size, 0, new Vector2(movedelta.x, 0), Mathf.Abs(movedelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null) {
            transform.Translate(movedelta.x * Time.deltaTime, 0, 0);
        }
    }

    protected abstract void ChangeSpriteDirection(Vector3 dir);

    protected virtual void Death(Damage dmg) {
        Destroy(this.gameObject);
    }

}
