using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LV2_TO_LV3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level3");
        }

    }
}
