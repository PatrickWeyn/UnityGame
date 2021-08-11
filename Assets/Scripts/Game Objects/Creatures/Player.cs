using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Creature {

    //Detection Aura

    private int experience;
    private Weapon weapon;
    public int unusedabilitypoints;

    private void Start() {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        experience = 0;
        STR = 1;
        DEX = 1;
        CON = 1;
        INT = 1;
        WIS = 1;
        CHA = 1;
        unusedabilitypoints = 5;
        weapon = gameObject.transform.Find("Weapon").GetComponent<Weapon>();
        GameManager.app.UI.SendMessage("UpdateAbilityScores");
        GameManager.app.UI.SendMessage("UpdateStats");
    }

    public void SetExperience(int xp) {
        experience += xp;
        GameManager.app.UI.SendMessage("UpdateStats");
    }

    public int GetExperience() {
        return experience;
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

        MoveCreature(new Vector3(x, y, 0));
    }

    public void AddScore(string ability) {
        if (unusedabilitypoints > 0) {
            unusedabilitypoints -= 1;
            switch (ability) {
                case "STR": STR += 1; break;
                case "DEX": DEX += 1; break;
                case "CON": CON += 1; break;
                case "INT": INT += 1; break;
                case "WIS": WIS += 1; break;
                case "CHA": CHA += 1; break;
            }
            GameManager.app.UI.SendMessage("UpdateAbilityScores");
        }
    }

    public Weapon GetWeapon() {
        return weapon;
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
    }
}
