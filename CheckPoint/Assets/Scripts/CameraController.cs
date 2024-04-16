using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Target to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset; // Follow the target with specified offset
        }
    }
}