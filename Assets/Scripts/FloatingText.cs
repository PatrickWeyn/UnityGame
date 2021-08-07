using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText
{
    public GameObject go;
    private Vector3 motion;
    private float duration;
    private float lastshown;

    public void Show(Vector3 motion, float duration) {
        this.motion = motion;
        this.duration = duration;
        lastshown = Time.time;

        go.SetActive(true);
    }

    public void Hide() {
        go.SetActive(false);
    }

    public void UpdateFloatingText() {
        if (Time.time - lastshown < duration) {
            go.transform.position += motion * 0.001f;
        } else {
            Hide();
        }
    }
}
