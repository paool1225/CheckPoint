using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatformController : MonoBehaviour
{
    public float riseSpeed = 2f; // Speed at which the platform rises
    public float respawnTime = 5f; // Time in seconds before the platform respawns after being destroyed
    public Color activeColor = Color.yellow; // Color when the player is on the platform and it's rising
    private Color originalColor; // To store the original color of the platform
    private Vector3 originalPosition; // Original position of the platform
    private bool playerOnPlatform = false; // Flag to check if the player is on the platform
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isDestroyed = false; // To keep track of the platform's state

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position; // Store the original position
        originalColor = spriteRenderer.color; // Store the original color of the platform
        rb.isKinematic = true; // Set the Rigidbody to kinematic since we are moving it manually
    }

    void Update()
    {
        if (playerOnPlatform && !isDestroyed)
        {
            // Change the platform's color to activeColor while the player is on it and it's rising
            spriteRenderer.color = activeColor;
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
        else
        {
            // Revert the color to original when the player is not on the platform
            spriteRenderer.color = originalColor;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true; // Player is on the platform, start rising
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            // Handle collision with objects tagged as 'Death' like spiked ceilings
            DestroyPlatform();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false; // Player has left the platform
        }
    }

    void DestroyPlatform()
    {
        gameObject.SetActive(false); // Hide the platform
        isDestroyed = true; // Mark as destroyed
        Invoke(nameof(RespawnPlatform), respawnTime); // Schedule respawn
    }

    void RespawnPlatform()
    {
        gameObject.SetActive(true); // Reactivate the platform
        transform.position = originalPosition; // Reset position to the original
        spriteRenderer.color = originalColor; // Reset to original color
        isDestroyed = false; // Reset the destroyed flag
    }
}
