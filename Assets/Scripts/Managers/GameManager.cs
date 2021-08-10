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
        Gamestate = 1;
        alliesinrange = new List<Ally>();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Dungeon_Entrance");
    }

    public void Update() {
        foreach (Ally ally in alliesinrange) {
            if (Vector3.Distance(player.transform.position, closestally.transform.position) > Vector3.Distance(player.transform.position, ally.transform.position)) {
                closestally = ally;
            }
        }
        Debug.Log("There are " + alliesinrange.Count() + " in range.");
    }

    public void TrackNearbyNPC(Ally ally) {
        if (!alliesinrange.Any()) {
            closestally = ally;
        }
        alliesinrange.Add(ally);
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
            UI.ShowDialogMenu(closestally.Dialog);
        }
    }

    public void CloseDialog() {
        Gamestate = 1;
        UI.HideDialogMenu();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        player.transform.position = pointofentry;
        ftm.transform.position = Vector3.zero;
    }

    //Variable getters and setters
    public int Gamestate { get => gamestate; set => gamestate = value; }
}
