using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)){
            switch (GameManager.app.UI.GetComponent<Animator>().GetBool("isActive")){
                case true: GameManager.app.UI.GetComponent<Animator>().SetBool("isActive", false);
                    break;
                case false: GameManager.app.UI.GetComponent<Animator>().SetBool("isActive", true);
                    break;
            }
        }
    }

    public void UpdateWeapon() {
        GameObject weaponpanel = transform.Find("Container").Find("CharacterWeapon").gameObject;
        Weapon weapon = GameManager.app.player.transform.Find("Weapon").GetComponent<Weapon>();
        weaponpanel.transform.Find("MinDmg").GetComponent<Text>().text = weapon.weapon.mindmg.ToString();
        weaponpanel.transform.Find("MaxDmg").GetComponent<Text>().text = (weapon.weapon.maxdmg-1).ToString();
        weaponpanel.transform.Find("Pushback").GetComponent<Text>().text = weapon.weapon.pushback.ToString();
        weaponpanel.transform.Find("Speed").GetComponent<Text>().text = weapon.weapon.cooldown.ToString();
        weaponpanel.transform.Find("WeaponImage").GetComponent<Image>().sprite = weapon.weapon.art;
    }

    public void UpdateStats() {

    }
}
