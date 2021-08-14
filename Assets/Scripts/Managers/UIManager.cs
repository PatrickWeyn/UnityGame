using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameObject characterscreen;

    private void Start() {
        this.characterscreen = gameObject.transform.Find("CharacterScreen").gameObject;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            switch (characterscreen.activeSelf) {
            case true:
                characterscreen.SetActive(false);
                break;
            case false:
                characterscreen.SetActive(true);
                break;
            }
        }
    }

    public void UpdateWeapon() {
        GameObject weaponpanel = transform.Find("CharacterScreen").Find("CharacterWeapon").gameObject;
        Weapon weapon = GameManager.app.player.transform.Find("Weapon").GetComponent<Weapon>();
        weaponpanel.transform.Find("MinDmg").GetComponent<Text>().text = weapon.weapon.mindmg.ToString();
        weaponpanel.transform.Find("MaxDmg").GetComponent<Text>().text = (weapon.weapon.maxdmg - 1).ToString();
        weaponpanel.transform.Find("Pushback").GetComponent<Text>().text = weapon.weapon.pushback.ToString();
        weaponpanel.transform.Find("Speed").GetComponent<Text>().text = weapon.weapon.cooldown.ToString();
        weaponpanel.transform.Find("WeaponImage").GetComponent<Image>().sprite = weapon.weapon.art;
    }

    public void UpdateAbilityScores() {
        GameObject abilityscorepanel = transform.Find("CharacterScreen").Find("CharacterAbilityScores").gameObject;
        abilityscorepanel.transform.Find("STR").GetComponent<Text>().text = GameManager.app.player.STR.ToString();
        abilityscorepanel.transform.Find("DEX").GetComponent<Text>().text = GameManager.app.player.DEX.ToString();
        abilityscorepanel.transform.Find("CON").GetComponent<Text>().text = GameManager.app.player.CON.ToString();
        abilityscorepanel.transform.Find("INT").GetComponent<Text>().text = GameManager.app.player.INT.ToString();
        abilityscorepanel.transform.Find("WIS").GetComponent<Text>().text = GameManager.app.player.WIS.ToString();
        abilityscorepanel.transform.Find("CHA").GetComponent<Text>().text = GameManager.app.player.CHA.ToString();
        abilityscorepanel.transform.Find("Explanation").GetComponent<Text>().text = "Unused Skill Points: " + GameManager.app.player.unusedabilitypoints.ToString();
        if (GameManager.app.player.unusedabilitypoints == 0) {
            foreach (Button b in FindObjectsOfType<Button>()) {
                b.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateStats() {
        GameObject statspanel = transform.Find("CharacterScreen").Find("CharacterStats").gameObject;
        statspanel.transform.Find("HP").GetComponent<Text>().text = GameManager.app.player.maxhealth.ToString();
        statspanel.transform.Find("XP").GetComponent<Text>().text = GameManager.app.player.GetExperience().ToString();
    }
}
