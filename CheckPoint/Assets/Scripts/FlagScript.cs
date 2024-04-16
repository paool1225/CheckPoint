using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagScript : MonoBehaviour
{
    public GameObject deathEffect;
    private Rigidbody2D rb;
    public bool isStuck = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !isStuck)
        {
            rb.velocity = Vector2.zero; // Stop the flag movement
            rb.isKinematic = true; // Make the flag kinematic to stop physics interactions
            isStuck = true;
        }
        else if (col.collider.CompareTag("Death"))
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject); // Destroy the flag
            ResetScene(); // Call to reset the scene
        }
    }

    void ResetScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetFlag()
    {
        rb.isKinematic = false;
        isStuck = false;
    }
}