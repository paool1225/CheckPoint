using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control_Flag : MonoBehaviour
{
    public Transform present;
    public Transform flag; // Target to follow
    public Transform pflag; // player flag when flag on land is gone
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target

    [SerializeField] FlagScript Alive; // gets variable to check if flag is still on level

    public void SetTarget(Transform newTarget)
    {
        present = newTarget;
    }

    void Update()
    {
        
            if (Alive.isPresent == true)
            {
                transform.position = Alive.flagPos + offset; //Follow the target with specified offset
            }
        else
        {
            SetTarget(pflag);
            transform.position = pflag.position + offset;
        }
       
    }
}
