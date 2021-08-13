using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Children
    private GameObject dialogpanel;
    private GameObject responsepanel;
    private GameObject characterscreen;

    //Dialog Handling
    private Ally conversationpartner;

    private void Start() {
        dialogpanel = transform.Find("DialogueBox").gameObject;
        characterscreen = transform.Find("CharacterScreen").gameObject;
        responsepanel = dialogpanel.transform.Find("PAN_Responses").Find("Viewport").Find("Content").gameObject;
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
        //TODO - optimize
        if (GameManager.app.player.unusedabilitypoints == 0) {
            foreach (Button b in FindObjectsOfType<Button>()) {
                b.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateStats() {
        GameObject statspanel = characterscreen.transform.Find("CharacterStats").gameObject;
        statspanel.transform.Find("HP").GetComponent<Text>().text = GameManager.app.player.maxhealth.ToString();
        statspanel.transform.Find("XP").GetComponent<Text>().text = GameManager.app.player.GetExperience().ToString();
    }

    //Dialogue Handling
    public void StartDialog() {
        //Apply correct sprite and name
        dialogpanel.transform.Find("IMG_NPCArt").GetComponent<Image>().sprite = conversationpartner.GetComponent<SpriteRenderer>().sprite;
        dialogpanel.transform.Find("LBL_NPCName").GetComponent<Text>().text = conversationpartner.name;

        //Show the dialogmenu
        StartCoroutine(HandleDialogText(conversationpartner.GetDialogs()[0].id));
        dialogpanel.SetActive(true);
    }

    public void SetConversationPartner(Ally closestally) {
        this.conversationpartner = closestally;
    }

    public void HideDialogMenu() {
        dialogpanel.SetActive(false);
    }

    public Dialog GetDialog(string id) {
        for (int counter = 0; counter < conversationpartner.GetDialogs().Count; counter++) {
            if (conversationpartner.GetDialogs()[counter].id == id) return conversationpartner.GetDialogs()[counter];
        }

        throw new MissingReferenceException("UIManager.cs - GetDialog() - value \"" + id + "\" is not found. Check the dialog file for " + conversationpartner.name + " and verify that there are no writing mistakes or formatting errors.");
    }

    public IEnumerator HandleDialogText(string identifier, string opinion = null, int value = 0) {
        //Local Variable declaration
        int counter;
        string msg;
        Color color;
        bool clickable;

        //Add value to conversationpartner's opinion score
        conversationpartner.AddOpinionScore(opinion, value);

        //1. Find the correct dialog (throws exception if none found)
        Dialog dialog = GetDialog(identifier);

        //2. Destroy the previous options
        for (counter = 0; counter < responsepanel.transform.childCount; counter++) {
            GameObject.Destroy(responsepanel.transform.GetChild(counter).gameObject);
        }

        //3. Show one line of dialog every two seconds
        for (counter = 0; counter < dialog.texts.Count(); counter++) {
            if (dialog.texts[counter].Contains("{")) {
                switch (conversationpartner.GetOpinion()) {
                case "relaxed":
                    dialogpanel.transform.Find("LBL_NPCText").GetComponent<Text>().text = dialog.texts[counter].Substring((dialog.texts[counter].IndexOf("{") + 1), dialog.texts[counter].IndexOf("|") - dialog.texts[counter].IndexOf("{") - 1);
                    break;
                case "neutral":
                    dialogpanel.transform.Find("LBL_NPCText").GetComponent<Text>().text = dialog.texts[counter].Substring((dialog.texts[counter].IndexOf("|") + 1), dialog.texts[counter].LastIndexOf("|") - dialog.texts[counter].IndexOf("|") - 1);
                    break;
                case "tense":
                    dialogpanel.transform.Find("LBL_NPCText").GetComponent<Text>().text = dialog.texts[counter].Substring((dialog.texts[counter].LastIndexOf("|") + 1), dialog.texts[counter].IndexOf("}") - dialog.texts[counter].LastIndexOf("|") - 1);
                    break;
                }

            } else {
                dialogpanel.transform.Find("LBL_NPCText").GetComponent<Text>().text = dialog.texts[counter];
            }
            if (counter != dialog.texts.Count() - 1 || dialog.options.Count == 0) {
                yield return new WaitForSeconds(2.0f);
            }
        }

        if (dialog.options.Count != 0) {
            //4. When the last line of dialog is shown, create the new options
            for (counter = 0; counter < dialog.options.Count; counter++) {

                //reset option variables;
                msg = "";
                color = Color.white;
                clickable = true;

                //Check if the player passes any ability checks that are required...
                if (dialog.options[counter].GetAbilityName() != null) {
                    if (GameManager.app.player.GetScore(dialog.options[counter].GetAbilityName()) < dialog.options[counter].GetAbilityValue()) {

                        //...Gray out the option, make it unclickable if he doesn't...
                        color = new Color(0.7f, 0.7f, 0.7f, 1);
                        clickable = false;
                    }

                    //..and add the text to show that this option requires an ability score check
                    msg += "[" + dialog.options[counter].GetFullAbilityName() + " " + dialog.options[counter].GetAbilityValue() + "] ";
                }

                //Create new gameobject for an option, and add it to the panel...
                GameObject txtobj = new GameObject();
                txtobj.transform.SetParent(responsepanel.transform);

                //...Position the gameobject properly on said panel...
                RectTransform rct = txtobj.AddComponent<RectTransform>();
                rct.sizeDelta = new Vector2(1445, 50);
                rct.localScale = Vector3.one;

                //...Add the option-text to the gameobject...
                Text txt = txtobj.AddComponent<Text>();
                txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                txt.fontSize = 28;
                txt.alignment = TextAnchor.MiddleLeft;
                txt.color = color;
                txt.text = (counter + 1).ToString() + ". " + msg + dialog.options[counter].GetText();

                //..make the button clickable if the required ability check, if any, was succesful.
                if (clickable) {
                    OptionButton btn = txtobj.AddComponent<OptionButton>();
                    btn.destination = dialog.options[counter].GetDestination();
                    //If the button adds or removes tension, add it to the button now.
                    if (dialog.options[counter].GetOpinionName() != null) {
                        btn.opinion = dialog.options[counter].GetOpinionName();
                        btn.value = dialog.options[counter].GetOpinionValue();
                        btn.onClick.AddListener(delegate { StartCoroutine(HandleDialogText(btn.destination, btn.opinion, btn.value)); });
                    } else {
                        btn.onClick.AddListener(delegate { StartCoroutine(HandleDialogText(btn.destination)); });
                    }
                }
            }
        } else {
            GameManager.app.CloseDialog();
        }
    }

    public void SelectDialogOption(int choice) {
        if (choice - 1 <= responsepanel.transform.childCount) {
            responsepanel.transform.GetChild(choice - 1).GetComponent<OptionButton>().onClick.Invoke();
        }
    }

    public void HandleCharacterScreen() {
        bool state = false;
        if (!characterscreen.activeSelf) {
            state = true;
        }
        characterscreen.SetActive(state);
    }

    public bool MenuActive() {
        return characterscreen.activeSelf;
    }
}
