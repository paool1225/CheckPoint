using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public AudioClip soundEffect; // Assign this in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Find the AudioSource component either on this GameObject or another one
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Optionally, search for the AudioSource anywhere in the scene if it's not on the same GameObject
            // audioSource = FindObjectOfType<AudioSource>();
            Debug.LogError("No AudioSource component found on the Door Trigger object.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}