using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC {
    //Constants
    private static float DETECTIONRANGE = 0.32f;

    //Test Data (delete in method Start())
    public Dialog dialog;

    private bool playerinrange;

    private void Start() {
        dialog = new Dialog(this.GetComponent<SpriteRenderer>().sprite, "Brent", "Halligan", "Hail traveler, what seems to be the issue?", "Nothing");
    }

    private void Update() {
        if (DETECTIONRANGE > Vector3.Distance(transform.position, GameManager.app.player.transform.position)) {
            if (!playerinrange) {
                playerinrange = true;
                Debug.Log("Player entered within range of " + name + "! Press F to interact!");
            }
            if (Input.GetKeyDown(KeyCode.F)) {
                GameManager.app.UI.SendMessage("UpdateDialogue", dialog);
                GameManager.app.UI.SendMessage("HandleMenu", KeyCode.F);
            }

        }
        else playerinrange = false;
    }
}
