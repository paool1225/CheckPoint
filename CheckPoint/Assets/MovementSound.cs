using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public PlayerController onTheGround;

    public AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (onTheGround.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                audioSrc.Play();
            }
        }

        else if (!onTheGround.isGrounded || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            audioSrc.Stop();
        }
    }
}
