using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private static readonly KeyCode[] keyCodes = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(k => ((int)k < (int)KeyCode.JoystickButton0)).ToArray();
    private List<KeyCode> codes = new List<KeyCode>();

    private void Update() {
        switch (GameManager.app.Gamestate) {
            //Default State
            case 1:
                DefaultStateInput();
                break;
            //Dialog State
            case 2:
                DialogStateInput();
                break;
        }
    }

    private void DefaultStateInput() {
        if (Input.GetKeyDown(KeyCode.F)) {
            GameManager.app.InitiateDialog();
        }
    }

    private void DialogStateInput() {
        if (Input.GetKeyDown(KeyCode.F)) {
            GameManager.app.CloseDialog();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameManager.app.SelectDialogOption(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GameManager.app.SelectDialogOption(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            GameManager.app.SelectDialogOption(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            GameManager.app.SelectDialogOption(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            GameManager.app.SelectDialogOption(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            GameManager.app.SelectDialogOption(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            GameManager.app.SelectDialogOption(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            GameManager.app.SelectDialogOption(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            GameManager.app.SelectDialogOption(9);
        }
    }
    /*
        private void UpdateKeyspressed() {
            if (Input.anyKeyDown) {
                for (int i = 0; i < keyCodes.Length; i++) {
                    KeyCode kc = keyCodes[i];
                    if (Input.GetKeyDown(kc)) {
                        codes.Add(kc);
                    }
                }
            }

            if (codes.Count > 0) {
                for (int i = 0; i < codes.Count; i++) {
                    KeyCode kc = codes[i];
                    if (Input.GetKeyUp(kc)) {
                        codes.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    */
}
