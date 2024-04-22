using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 currentTarget;

    void Start()
    {
        currentTarget = pointA.position; // Start by moving towards point A
    }

    void Update()
    {
        MoveBetweenPoints();
    }

    void MoveBetweenPoints()
    {
        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Check if we have reached the current target
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            // If the current target is point A, switch to point B, and vice versa
            currentTarget = (currentTarget == pointA.position) ? pointB.position : pointA.position;
        }
    }
}