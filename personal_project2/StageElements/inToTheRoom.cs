using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inToTheRoom : MonoBehaviour
{
    public GameObject player;
    public CameraController cameraController;

    void Start()
    {
        player = GameObject.Find("Dust");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.transform.position = new Vector3(21, -34, 0);
            cameraController.center.y = -30f;
            cameraController.mapSize.x = 22f;
        }
    }

}
