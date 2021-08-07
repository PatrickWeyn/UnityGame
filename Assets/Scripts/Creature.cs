using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Creature : MonoBehaviour {
    public float speedx;
    public float speedy;
    private RaycastHit2D hit;

    protected virtual void Start() {
    }

    protected virtual void FixedUpdate() {

        //Capture Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Determine the difference between our current position and the new position
        Vector3 movedelta = new Vector3(x * speedx, y * speedy, 0);

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

}
