using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Transition
{
    public string sceneName;

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            SceneManager.LoadScene(sceneName);
            collision.gameObject.transform.position = landingpos;
        }
    }
}
