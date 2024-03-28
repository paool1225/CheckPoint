using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    private GameObject player; // Reference to the player inside the trigger

    void Update()
    {
        // If the player is in the trigger, check their flag status
        if (player != null)
        {
            UpdateWallState();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject; // Set the player reference when they enter the trigger
            UpdateWallState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null; // Clear the player reference when they exit the trigger
            // Optionally reset wall state here if needed
        }
    }

    void UpdateWallState()
    {
        // Assumes player has a PlayerController component
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            // Directly manipulate the collider component of the barrier here
            // For example, enabling/disabling a collider or activating/deactivating a GameObject
            // This is a placeholder for where you'd implement the logic to block/unblock the player
            bool shouldBlock = playerController.hasFlag;
            // Adjust this to control your actual barrier, such as enabling/disabling a collider or GameObject
            GetComponent<Collider2D>().isTrigger = !shouldBlock;
        }
    }
}