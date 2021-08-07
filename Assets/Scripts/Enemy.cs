using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {
    public float engageange;
    public float chaserange;
    public Vector3 startpos;
    private int status;

    private void FixedUpdate() {
        switch (status) {
            // Idle
            case 0:
                if (Vector3.Distance(GameManager.app.player.transform.position, transform.position) < engageange) {
                    status = 1;
                    startpos = transform.position;
                    GameManager.app.ftm.ShowMessage("Sqwuuueeeek!", "dmg", transform.position);
                }
                break;
            // Chasing
            case 1:
                if (Vector3.Distance(GameManager.app.player.transform.position, startpos) < chaserange) {
                    MoveCreature((GameManager.app.player.transform.position - transform.position).normalized);
                }
                else {
                    status = 2;
                }
                break;
            // Returning
            case 2:
                if (Vector3.Distance(GameManager.app.player.transform.position, startpos) > chaserange) {
                    MoveCreature((startpos - transform.position).normalized);
                    if (Vector3.Distance(transform.position, startpos) < 0.01) {
                        status = 0;
                    }
                }
                else {
                    status = 1;
                }
                break;
        }
    }
}
