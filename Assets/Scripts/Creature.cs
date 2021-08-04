using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{

    public float speedx;
    public float speedy;

    protected virtual void FixedUpdate() {

        //Capture Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Determine the difference between our current position and the new position
        Vector3 movedelta = new Vector3(x * speedx, y * speedy, 0);

        //Swap sprite direction based on mouse position
        if ((Input.mousePosition.x- Screen.width / 2) <0) {
            transform.localScale = new Vector3(-1, 1, 0);
        }
        else {
            transform.localScale = new Vector3(1, 1, 0);
        }

        string githubtest = "test";
    }

}
