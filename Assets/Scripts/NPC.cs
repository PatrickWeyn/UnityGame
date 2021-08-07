using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Creature {
    protected override void ChangeSpriteDirection(Vector3 dir) {
        //Swap sprite direction based on mouse position
        if (dir.x < 0) transform.localScale = new Vector3(-1, 1, 0);
        if (dir.x > 0) transform.localScale = new Vector3(1, 1, 0);
    }
}
