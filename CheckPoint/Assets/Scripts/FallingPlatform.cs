using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pointA;
    public GameObject platform;
    private Vector3 currentTarget;
    public float speed = 2f;
    public GameObject currentPlatform;

    void Start()
    {
        currentTarget = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlatform == null)
        { 
            currentPlatform = Instantiate(platform, transform.position, Quaternion.identity, transform);
        }
        Move();
    }

    void Move()
    {
        // Move towards the current target
        currentPlatform.transform.position = Vector3.MoveTowards(currentPlatform.transform.position, currentTarget, speed * Time.deltaTime);

        // Check if we have reached the current target
        if (Vector3.Distance(currentPlatform.transform.position, currentTarget) < 0.1f)
        {
           // kill the platform and generate a new one
           Destroy(currentPlatform);
        }
    }
}
