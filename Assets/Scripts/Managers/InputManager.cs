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
