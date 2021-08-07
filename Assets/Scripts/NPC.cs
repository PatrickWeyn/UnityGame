using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Creature {

    public int xp;

    protected override void ChangeSpriteDirection(Vector3 dir) {
        if (dir.x < 0) transform.localScale = new Vector3(1, 1, 0);
        if (dir.x >= 0) transform.localScale = new Vector3(-1, 1, 0);
    }

    protected override void Death(Damage dmg) {
        base.Death(dmg);
        if (dmg.source.name == GameManager.app.player.name) GameManager.app.player.experience += xp;
        GameManager.app.ftm.ShowMessage("+" + xp + " XP", "xp", transform.position);

    }
}
