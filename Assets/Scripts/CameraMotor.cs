using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Player player;

    public void LateUpdate() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
