using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Manager : MonoBehaviour
{
    public GameObject stage5_is;

    public GameObject player;
    public CameraController cameraController;

    public GameObject desk;
    public GameObject bgMusic;

    void Start()
    {
        stage5_is = GameObject.Find("stage5_Is");

        if (stage5_is.GetComponent<Stage5_isDone>().isDone)
        {
            desk.SetActive(false);

            player.transform.position = new Vector3(0.76f, -34, 0);
            cameraController.center.y = -30f;
            cameraController.mapSize.x = 22f;
        }

    }
}
