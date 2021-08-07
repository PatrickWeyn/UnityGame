using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Creature {

    private void Start() {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    protected override void ChangeSpriteDirection(Vector3 dir) {
        //Swap sprite direction based on mouse position
        if ((Input.mousePosition.x - Screen.width / 2) < 0) {
            transform.localScale = new Vector3(-1, 1, 0);
        }
        else {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }

    private void FixedUpdate() {
        //Capture Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        this.MoveCreature(new Vector3(x, y, 0));
    }
}
