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
        case 2:
            DialogInput();
            break;
        case 3:
            MenuInput();
            break;
        }
    }

    private void DefaultInput() {
        if (Input.GetKeyDown(KeyCode.B)) {
            GameManager.app.ShowCharacterScreen();
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            GameManager.app.ShowDialogScreen();
        }
    }

    private void DialogInput() {
        if (Input.GetKeyDown(KeyCode.F)) {
            GameManager.app.HideDialogScreen();
        }
    }

    private void MenuInput() {
        if (Input.GetKeyDown(KeyCode.B)) {
            GameManager.app.HideCharacterScreen();
        }
    }
}
