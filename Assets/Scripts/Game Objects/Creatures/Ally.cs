using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC {
    //Constants
    private const float DETECTIONRANGE = 0.32f;

    //Unity-Accessible Variables
    public string dialoguefile;

    //Dialog Variables
    private bool playerinrange;
    private List<Dialog> dialogs;
    private int tension = 1;
    private int relaxation = 1;

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

    public void AddOpinionScore(string opinion, int value) {
        switch (opinion) {
        case "tension":
            tension += value;
            Debug.Log("influence: " + name + " -" + value);
            break;
        case "relaxation":
            relaxation += value;
            Debug.Log("influence: " + name + " +" + value);
            break;
        }
    }

    public string GetOpinion() {
        double score = relaxation / (relaxation / tension);
        if (score > 0.7) {
            return "relaxed";
        } else if (score < 0.3) {
            return "tense";
        }
        return "neutral";
    }
}
