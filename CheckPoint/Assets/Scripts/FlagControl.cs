using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagControl : MonoBehaviour
{
    public GameObject deathEffect;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Death"))
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject); // Destroy the flag
            ResetScene(); // Call to reset the scene
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
