using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Children
    private GameObject dialogpanel;
    //Dialog Handling
    private Ally conversationpartner;

    private void Start() {
        dialogpanel = transform.Find("DialogueBox").gameObject;

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

    //Dialogue Handling
    public void InitializeDialog(Ally conversationpartner) {
        //Apply correct sprite and name
        dialogpanel.transform.Find("IMG_NPCArt").GetComponent<Image>().sprite = conversationpartner.GetComponent<SpriteRenderer>().sprite;
        dialogpanel.transform.Find("LBL_NPCName").GetComponent<Text>().text = conversationpartner.name;

        //Store dialogs for this conversation
        this.conversationpartner = conversationpartner;

        //Fill the dialogbox and options with the contents of the first dialog
        StartCoroutine(HandleDialogText("intro"));

        //Show the dialogmenu
        dialogpanel.SetActive(true);
    }

    public void HideDialogMenu() {
        transform.Find("DialogueBox").gameObject.SetActive(false);
    }

    public Dialog GetDialog(string id) {
        foreach (Dialog d in conversationpartner.GetDialogs()) {
            if (d.id == id) return d;
        }
        return null;
    }

    IEnumerator HandleDialogText(string identifier) {
        //Find the correct dialog
        Dialog dialog = GetDialog(identifier);

        //Removes all previous options
        foreach (Transform child in dialogpanel.transform.Find("PAN_Responses").gameObject.transform) {
            GameObject.Destroy(child.gameObject);
        }

        //Handle dialog text (TODO)
        for (int i = 0; i < dialog.texts.Count(); i++) {
            dialogpanel.transform.Find("LBL_NPCText").GetComponent<Text>().text = dialog.texts[i];
            if (i != dialog.texts.Count() - 1) {
                yield return new WaitForSeconds(2.0f);
            }
        }

        //Create new options
        for (int i = 0; i < dialog.options.Count(); i++) {
            //Create new gameobject for an option, and add it to the panel
            GameObject txtobj = new GameObject();
            txtobj.transform.SetParent(dialogpanel.transform.Find("PAN_Responses").gameObject.transform);
            //Position the gameobject properly on said panel
            RectTransform rct = txtobj.AddComponent<RectTransform>();
            rct.anchorMin = new Vector2(0, 1);
            rct.anchorMax = new Vector2(0, 1);
            rct.pivot = new Vector2(0, 1);
            rct.anchoredPosition = new Vector2(0, i * -50);
            rct.sizeDelta = new Vector2(1515, 50);
            rct.localScale = Vector3.one;
            //Add Text to the gameobject
            Text txt = txtobj.AddComponent<Text>();
            txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            txt.fontSize = 28;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = Color.white;
            txt.text = (i + 1).ToString() + ". " + dialog.options[i].text;
            //Make button clickable
            OptionButton btn = txtobj.AddComponent<OptionButton>();
            btn.destination = dialog.options[i].destination;
            btn.onClick.AddListener(delegate { StartCoroutine(HandleDialogText(btn.destination)); });
        }
    }
}
