using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformController : MonoBehaviour
{
    public float delay = 2f; // Time in seconds before the platform reacts
    public float respawnTime = 5f; // Time in seconds before the platform respawns after being destroyed
    private float timer; // Timer to track the delay
    private bool playerOnPlatform = false;
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool shouldRise = false; // To check if the platform should rise
    private SpriteRenderer spriteRenderer;
    public Color fallingColor = Color.red; // Color when the platform starts falling
    private Color originalColor; // Store the original color
    private bool isDestroyed = false; // Track if the platform is destroyed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Ensure the platform starts as kinematic
        originalPosition = transform.position; // Store the original position of the platform
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color
    }

    void Update()
    {
        if (!isDestroyed)
        {
            if (playerOnPlatform)
            {
                timer += Time.deltaTime;
                if (timer >= delay)
                {
                    // Platform starts falling
                    rb.isKinematic = false;
                    rb.gravityScale = 1; // Adjust gravity scale if needed
                    spriteRenderer.color = fallingColor; // Change color to indicate falling
                }
            }
            else
            {
                if (shouldRise)
                {
                    RisePlatform();
                }
                timer = 0; // Reset the timer
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            shouldRise = false; // Stop rising as the player is on the platform
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            // Destroy and respawn the platform
            DestroyPlatform();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            shouldRise = true; // Start rising when the player leaves
        }
    }

    void DestroyPlatform()
    {
        spriteRenderer.enabled = false;
        rb.isKinematic = true; // Prevent it from falling any further
        GetComponent<Collider2D>().enabled = false;
        isDestroyed = true;
        Invoke(nameof(RespawnPlatform), respawnTime); // Respawn after specified time
    }

    void RespawnPlatform()
    {
        transform.position = originalPosition; // Reset position
        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        spriteRenderer.color = originalColor; // Reset to original color
        isDestroyed = false; // Reset destruction state
    }

    void RisePlatform()
    {
        // Reset the platform to rise back up
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * 2); // Adjust speed as necessary
        if (transform.position == originalPosition)
        {
            rb.isKinematic = true; // Make it kinematic again once it reaches the original position
            rb.gravityScale = 0; // Reset gravity scale
            spriteRenderer.color = originalColor; // Reset to original color
            shouldRise = false; // Stop the rise process
        }
    }
}
