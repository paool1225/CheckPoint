using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFallingPlatform : MonoBehaviour
{
    public float amplitude = 0.5f; // how far it's gonna move
    public float speed = 1.0f; // how fast it's gonna move

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // sin wave calculation
        float verticalOffset = Mathf.Sin(Time.time * speed) * amplitude;

        // object position update
        transform.position = initialPosition + new Vector3(0, verticalOffset, 0);
    }
}
