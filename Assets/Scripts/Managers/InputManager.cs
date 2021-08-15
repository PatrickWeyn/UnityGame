using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        switch (GameManager.app.GetGamestate()) {
        case 1:
            DefaultInput();
            break;
        case 3:
            MenuInput();
            break;
        }
    }

    private void DefaultInput() {
        if (Input.GetKeyDown(KeyCode.B)) {
            GameManager.app.ToggleCharacterScreen();
        }
    }

    private void MenuInput() {
        if (Input.GetKeyDown(KeyCode.B)) {
            GameManager.app.ToggleCharacterScreen();
        }
    }
}
