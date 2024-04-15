using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCameraFollow : MonoBehaviour
{
    public GameObject flag; // This will be set through public access or methods in the game manager or player controller.

    public Vector3 offset = new Vector3(0, 5, -10); // Customizable offset from the flag.

    void Update()
    {
        if (flag != null)
        {
            // Ensure the camera smoothly follows the flag
            Vector3 targetPosition = flag.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.LookAt(flag.transform);
        }
        else
        {
            Debug.LogError("Flag reference is missing in MiniCameraFollow.");
        }
    }

    public void SetFlag(GameObject newFlag)
    {
        if (newFlag != null)
        {
            flag = newFlag;
        }
        else
        {
            Debug.LogError("Attempted to set a null flag in MiniCameraFollow.");
        }
    }
}
