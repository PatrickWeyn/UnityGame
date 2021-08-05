using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager app;
    public Player player;
    public Camera cam;
    public Vector2 position;

    public void Awake() {
        if (app != null) {
            return;
        }
        app = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(cam);

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Dungeon_Entrance");
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        player.transform.position = position;
    }
}
