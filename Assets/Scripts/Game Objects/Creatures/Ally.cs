using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC {
    //Variables
    private const float DETECTIONRANGE = 2.0f;

    private bool inrange;

    private void Update() {
        if (DETECTIONRANGE > Vector3.Distance(transform.position, GameManager.app.player.transform.position)) {
            if (!inrange) {
                GameManager.app.AddAlly(this);
                inrange = true;
            }
        } else if (inrange == true) {
            inrange = false;
            GameManager.app.RemoveAlly(this);
        }
    }
}
