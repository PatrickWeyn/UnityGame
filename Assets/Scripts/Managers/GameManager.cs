using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager app;
    public Camera cam;
    public FTManager ftm;
    public Vector2 pointofentry;
    public UIManager UI;
    public Player player;
    public Weapon weapon;
    public EventSystem eventsys;
    public InputManager im;
    public FileParser fp;

    private List<Ally> alliesinrange;
    private Ally closestally;
    private int gamestate;

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
        DontDestroyOnLoad(im);
        DontDestroyOnLoad(fp);
        Gamestate = 1;
        alliesinrange = new List<Ally>();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Dungeon_Entrance");
    }

    public void Update() {
        for (int i = 0; i < alliesinrange.Count; i++) {
            if (Vector3.Distance(player.transform.position, closestally.transform.position) > Vector3.Distance(player.transform.position, alliesinrange[i].transform.position)) {
                closestally = alliesinrange[i];
            }
        }
    }

    public void TrackNearbyNPC(Ally ally) {
        if (!alliesinrange.Any()) {
            closestally = ally;
        }
        alliesinrange.Add(ally);
        if (ally.GetDialogs() == null) {
            ally.SetDialogs(fp.ParseDialog(ally.dialoguefile));
        }
    }

    public void UntrackNearbyNPC(Ally ally) {
        alliesinrange.Remove(ally);
        if (!alliesinrange.Any()) {
            closestally = null;
        }
    }

    public void InitiateDialog() {
        if (closestally != null) {
            Gamestate = 2;
            UI.InitializeDialog(closestally);
        }
    }

    public void CloseDialog() {
        Gamestate = 1;
        UI.HideDialogMenu();
    }

    public void SelectDialogOption(int choice) {
        UI.SelectDialogOption(choice);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        player.transform.position = pointofentry;
        ftm.transform.position = Vector3.zero;
    }

    public void HandleMenu() {
        if (UI.MenuActive()) {
            Gamestate = 1;
        }
        else { 
        Gamestate = 3;
        }
        UI.HandleCharacterScreen();
    }

    //Variable getters and setters
    public int Gamestate { get => gamestate; set => gamestate = value; }
}
