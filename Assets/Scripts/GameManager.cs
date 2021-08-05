using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager app;
    public Player player;
    public Camera camera;

    public void Awake() {
        if (app != null) {
            return;
        }

        app = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(camera);
    }
}
