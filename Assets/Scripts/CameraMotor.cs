using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public void LateUpdate() {
        transform.position = new Vector3(GameManager.app.player.transform.position.x, GameManager.app.player.transform.position.y, -10);
    }
}
