using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC {
    //Constants
    private static float DETECTIONRANGE = 0.32f;

    //Variables
    public string name;
    
    //Dialog
    private bool playerinrange;
    public string dialoguefile;
    private List<Dialog> dialogs;

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

    public void SetDialogs(List<Dialog> dialogs) {
        this.dialogs = dialogs;
    }

    public List<Dialog> GetDialogs() {
        return dialogs;
    }
}
