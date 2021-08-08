using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
