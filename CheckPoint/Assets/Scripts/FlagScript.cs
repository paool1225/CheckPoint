using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    public GameObject deathEffect;

     void OnCollisionEnter2D(Collision2D col){
        if(col.collider.transform.tag == "Death"){
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
