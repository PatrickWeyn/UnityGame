using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager app;
    public Player player;
    public Camera cam;
    public FTManager ftm;
    public Weapon weapon;
    public Vector2 pointofentry;
    public Canvas UI;
    public GameObject BackgroundExit;
    public GameObject UIContainer;
    public GameObject Char_Stat;
    public GameObject Char_Equip;
    public GameObject Char_Stat2;


    public void Awake() {
        if (app != null) {
            return;
        }
        app = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(cam);
        DontDestroyOnLoad(ftm);
        DontDestroyOnLoad(weapon);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(BackgroundExit);
        DontDestroyOnLoad(UIContainer);
        DontDestroyOnLoad(Char_Stat);
        DontDestroyOnLoad(Char_Equip);
        DontDestroyOnLoad(Char_Stat2);

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Dungeon_Entrance");
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        player.transform.position = pointofentry;
        ftm.transform.position = Vector3.zero;
    }
}
