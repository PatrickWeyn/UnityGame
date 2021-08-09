using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager app;
    public Camera cam;
    public FTManager ftm;
    public Vector2 pointofentry;
    public Canvas UI;
    public GameObject UIContainer;
    public GameObject Char_Stat;
    public GameObject Char_Equip;
    public GameObject Char_Stat2;
    public Player player;
    public Weapon weapon;
    public EventSystem eventsys;


    public void Awake() {
        if (app != null) {
            return;
        }
        app = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(cam);
        DontDestroyOnLoad(ftm);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(UIContainer);
        DontDestroyOnLoad(Char_Stat);
        DontDestroyOnLoad(Char_Equip);
        DontDestroyOnLoad(Char_Stat2);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(weapon);
        DontDestroyOnLoad(eventsys);

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Dungeon_Entrance");
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        player.transform.position = pointofentry;
        ftm.transform.position = Vector3.zero;
    }
}
