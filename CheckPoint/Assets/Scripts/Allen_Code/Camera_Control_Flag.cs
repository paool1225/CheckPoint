using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control_Flag : MonoBehaviour
{
    public Transform flag; // Target to follow
    public Transform flag2; 
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target

    public void SetTarget(Transform newTarget)
    {
        flag = newTarget;
    }

    void LateUpdate()
    {
        if (flag != null)
        {
            transform.position = flag.position + offset; //Follow the target with specified offset
        }
        if (flag2 == null)
        {
            transform.position = flag2.position + offset;
        }
       
    }
}
