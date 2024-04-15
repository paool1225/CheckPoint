using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagInteraction : MonoBehaviour
{
    public Transform player; // Assign player's transform
    public Transform flag; // Assign flag's transform
    public CameraController flagCameraFollow; // Main camera follow script
    public Camera flagMiniCamera; // Reference to the second camera

    void Start()
    {
        if (flagMiniCamera != null)
        {
            flagMiniCamera.transform.LookAt(flag); // Ensure the camera looks at the flag at the start
        }
    }

    public void PickupFlag()
    {
        flagCameraFollow.SetTarget(player); // Main camera follows player
        // No change needed for mini camera, it should always look at the flag
    }

    public void ThrowFlag()
    {
        flagCameraFollow.SetTarget(flag); // Main camera follows the thrown flag
        // Mini camera continues to look at the flag
    }

    void Update()
    {
        if (flagMiniCamera != null && flag != null)
        {
            flagMiniCamera.transform.LookAt(flag); // Constantly update to look at the flag
        }
    }
}