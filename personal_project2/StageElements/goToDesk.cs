using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToDesk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Stage5")
            {
                Debug.Log("책상으로 이동함.");
                // Stage5_Desk 으로 이동
                SceneManager.LoadScene("Stage5_Desk");
            }
        }
    }
}
