using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

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

    public void UpdateDialogue(Dialog dialog) {
        GameObject dialoguepanel = transform.Find("DialogueBox").gameObject;
        dialoguepanel.transform.Find("IMG_NPCArt").GetComponent<Image>().sprite = dialog.Character;
        dialoguepanel.transform.Find("LBL_NPCName").GetComponent<Text>().text = dialog.Firstname + " " + dialog.Lastname;
        dialoguepanel.transform.Find("LBL_NPCText").GetComponent<Text>().text = dialog.Response;
        //Create new gameobject for an option
        GameObject txtobj = new GameObject();
        txtobj.transform.SetParent(dialoguepanel.transform.Find("PAN_Responses").gameObject.transform);
        //Add RectTransform
        RectTransform rct = txtobj.AddComponent<RectTransform>();
        rct.anchorMin = new Vector2(0, 1);
        rct.anchorMax = new Vector2(0, 1);
        rct.pivot = new Vector2(0, 1);
        rct.anchoredPosition = Vector2.zero;
        rct.sizeDelta = new Vector2(1515, 50);
        rct.localScale = Vector3.one;
        //Add Text
        Text txt = txtobj.AddComponent<Text>();
        txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txt.fontSize = 28;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.color = Color.white;
        txt.text = dialog.Option;
    }

    public void ShowDialogMenu(Dialog dialog) {
        UpdateDialogue(dialog);
        transform.Find("DialogueBox").gameObject.SetActive(true);
    }

    public void HideDialogMenu() {
        transform.Find("DialogueBox").gameObject.SetActive(false);
    }
}
