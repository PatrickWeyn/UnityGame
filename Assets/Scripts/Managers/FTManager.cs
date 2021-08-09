using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTManager : MonoBehaviour {
    private List<FloatingText> floatingtexts = new List<FloatingText>();
    public GameObject ftprefab;

    private FloatingText GetFloatingText() {
        FloatingText ft = floatingtexts.Find(t => !t.go.activeSelf);
        
        if (ft == null) {
            ft = new FloatingText();
            ft.go = Instantiate(ftprefab);
            ft.go.transform.SetParent(this.transform);
            floatingtexts.Add(ft);
        }
        return ft;
    }

    public void ShowMessage(string msg, string msgtype, Vector2 pos) {
        FloatingText ft = GetFloatingText();
        ft.go.GetComponent<Text>().text = msg;
        ft.go.GetComponent<Text>().transform.position = pos;
        switch (msgtype) {
            case "dmg": ft.go.GetComponent<Text>().color = Color.red; break;
            case "xp": ft.go.GetComponent<Text>().color = Color.yellow; break;
            case "hp": ft.go.GetComponent<Text>().color = Color.green; break;
        }
        ft.Show(Vector3.up, 2.0f);
    }

    public void Update() {
        foreach (FloatingText ft in floatingtexts) {
            ft.UpdateFloatingText();
        }
    }
}
