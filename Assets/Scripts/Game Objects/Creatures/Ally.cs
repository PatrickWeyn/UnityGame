using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC {
    //Constants
    private static float DETECTIONRANGE = 0.32f;

    //Test Data (delete in method Start())
    private Dialog dialog;

    private bool playerinrange;

    private void Start() {
        dialog = new Dialog(this.GetComponent<SpriteRenderer>().sprite, "Brent", "Halligan", "Hail traveler, what seems to be the issue?", "Nothing");
    }

    private void Update() {
        if (DETECTIONRANGE > Vector3.Distance(transform.position, GameManager.app.player.transform.position)) {
            if (!playerinrange) {
                playerinrange = true;
                GameManager.app.TrackNearbyNPC(this);
            }
        } else if (playerinrange) {
            playerinrange = false;
            GameManager.app.UntrackNearbyNPC(this);
        }
    }


    //Variable Getters and Setters
    public Dialog Dialog { get => dialog; set => dialog = value; }
}
