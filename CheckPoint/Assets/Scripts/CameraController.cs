using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float p;
    // Update is called once per frame
    void Update()
    {

        transform.position += (Vector3)(Vector2)(player.transform.position - transform.position) * p * Time.deltaTime;
    }
}
