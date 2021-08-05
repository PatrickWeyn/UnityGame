using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Vector2 landingpos;
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Player") GameManager.app.player.transform.position = landingpos;
    }
}
