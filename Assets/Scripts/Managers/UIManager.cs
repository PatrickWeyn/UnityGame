using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameObject characterscreen;
    private GameObject dialogscreen;
    private GameObject responsescreen;

    private void Start() {
        characterscreen = transform.Find("CharacterScreen").gameObject;
        dialogscreen = transform.Find("DialogueBox").gameObject;
        responsescreen = dialogscreen.transform.Find("PAN_Responses").transform.Find("Viewport").transform.Find("Content").gameObject;
    }

    public void SetCharacterScreenVisible(bool visible) {
        characterscreen.SetActive(visible);
    }

    public void SetDialogScreenVisible(bool visible) {
        dialogscreen.SetActive(visible);
    }

    public void UpdateWeapon() {
        GameObject weaponpanel = characterscreen.transform.Find("CharacterWeapon").gameObject;
        Weapon weapon = GameManager.app.player.GetWeapon();
        weaponpanel.transform.Find("MinDmg").GetComponent<Text>().text = weapon.weapon.mindmg.ToString();
        weaponpanel.transform.Find("MaxDmg").GetComponent<Text>().text = (weapon.weapon.maxdmg - 1).ToString();
        weaponpanel.transform.Find("Pushback").GetComponent<Text>().text = weapon.weapon.pushback.ToString();
        weaponpanel.transform.Find("Speed").GetComponent<Text>().text = weapon.weapon.cooldown.ToString();
        weaponpanel.transform.Find("WeaponImage").GetComponent<Image>().sprite = weapon.weapon.art;
    }

    public void UpdateAbilityScores() {
        GameObject abilityscorepanel = characterscreen.transform.Find("CharacterAbilityScores").gameObject;
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
