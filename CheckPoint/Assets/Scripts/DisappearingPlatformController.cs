using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformController : MonoBehaviour
{
    public float interval = 2f; // Time in seconds each state lasts
    private float nextTimeToSwitch; // Time when the platform will switch state
    private SpriteRenderer spriteRenderer;
    private Collider2D myCollider; // Renamed from collider2D to myCollider

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>(); // Updated variable name here
        nextTimeToSwitch = Time.time + interval; // Initialize the switch time
    }

    void Update()
    {
        if (Time.time >= nextTimeToSwitch)
        {
            // Toggle visibility and collider activation
            spriteRenderer.enabled = !spriteRenderer.enabled;
            myCollider.enabled = !myCollider.enabled; // Updated variable name here
            nextTimeToSwitch = Time.time + interval; // Set the next switch time
        }
    }
}