using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LV1_TO_LV2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level2");
        }

    }
}
