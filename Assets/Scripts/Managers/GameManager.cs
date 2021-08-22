using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //Variables
    public Vector2 pointofentry;
    private int gamestate;

    //DDOL
    public static GameManager app;
    public Camera cam;
    public FTManager ftm;
    public UIManager UI;
    public Player player;
    public EventSystem eventsys;

    //Objects only to be interacted with by the GameManager
    private InputManager im;
    private FileParser fp;
    private List<Ally> alliesinrange;
    private Ally conversationpartner;

    public void Awake() {
        if (app != null) {
            return;
        }
        app = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(cam);
        DontDestroyOnLoad(ftm);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(eventsys);
        im = GameManager.app.gameObject.AddComponent<InputManager>();
        fp = new FileParser();
        alliesinrange = new List<Ally>();

        gamestate = 1;
        fp.InitializeParser("oldman.xml");
        fp.GetDialog("intro");

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Dungeon_Entrance");
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        player.transform.position = pointofentry;
        ftm.transform.position = Vector3.zero;
    }

    public void ShowCharacterScreen() {
        UI.UpdateWeapon();
        UI.UpdateStats();
        UI.UpdateAbilityScores();
        UI.SetCharacterScreenVisible(true);
        gamestate = 3;

    }

    public void HideCharacterScreen() {
        UI.SetCharacterScreenVisible(false);
        gamestate = 1;
    }

    public void ShowDialogScreen() {
        UI.SetDialogScreenVisible(true);
        gamestate = 2;
    }

    public void HideDialogScreen() {
        UI.SetDialogScreenVisible(false);
        gamestate = 1;
}

    public int GetGamestate() {
        return gamestate;
    }

    public void AddAlly(Ally ally) {
        if (alliesinrange.Count == 0) {
            conversationpartner = ally;
        }
        alliesinrange.Add(ally);
        Debug.Log("Player entered within range of " + ally.name + ".");
    }

    public void RemoveAlly(Ally ally) {
        alliesinrange.Remove(ally);
        if (alliesinrange.Count == 0) {
            conversationpartner = null;
        }
        Debug.Log("Player left range of " + ally.name + ".");
    }
}
