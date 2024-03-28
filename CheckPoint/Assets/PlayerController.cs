using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // TODO maybe make throw velocity not additive to other velocity because then player can go very fast (or maybe this is intended)
    public GameObject visuals;
    public GameObject deathEffect;
    public GameObject flagInstance;
    public GameObject headSprite;
    public GameObject flagSprite;


    public GameObject flagPrefab;
    public float throwVelocity;
    public bool hasFlag = false;
    public KeyCode jump;
    public KeyCode left;
    public KeyCode right;

    public float maxHorizontalSpeed;
    public float horizontalAccel;

    public GameObject groundCheck1;
    public GameObject groundCheck2;
    public bool isGrounded = false;
    
    public float jumpVelocity = 10;

    public bool isDead = false;

    void Start()
    {
        
    }

    void Update(){
        if(!isDead){
            // Flag visual
            flagSprite.SetActive(hasFlag);

            // Flag throwing
            Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = (mousePosition - (Vector2)transform.position).normalized;
            if(Input.GetKey(KeyCode.Mouse0) && hasFlag){
                hasFlag = false;
                ignoreNextFlagCollision = true;
                flagInstance = Instantiate(flagPrefab, transform.position, transform.rotation);
                flagInstance.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (mouseDir * throwVelocity);
            }

            // Looking
            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            headSprite.transform.eulerAngles = new Vector3(0, 0, angle);
            
            if(Input.GetKey(jump) && isGrounded){
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpVelocity);
            }
        }
    }

    void FixedUpdate()
    {
        if(!isDead){
            // Ground check
            isGrounded = false;
            RaycastHit2D hit1 = Physics2D.Raycast(groundCheck1.transform.position, Vector3.down, 0.1f);
            if(hit1.collider != null){
                if(hit1.collider.transform.tag == "Ground"){
                    isGrounded = true;
                }
            }
            RaycastHit2D hit2 = Physics2D.Raycast(groundCheck2.transform.position, Vector3.down, 0.1f);
            if(hit2.collider != null){
                if(hit2.collider.transform.tag == "Ground"){
                    isGrounded = true;
                }
            }

            // if(isGrounded){
            //     if(!Input.GetKey(left) && !Input.GetKey(right)){
            //         GetComponent<Rigidbody2D>().drag = groundDragNoInput;
            //     } else {
            //         GetComponent<Rigidbody2D>().drag = groundDrag;
            //     }
            // } else {
            //     GetComponent<Rigidbody2D>().drag = airDrag;
            // }

            // Physics Controls
            float horizontalVel = GetComponent<Rigidbody2D>().velocity.x;
            float horizontalForce = horizontalAccel * GetComponent<Rigidbody2D>().mass;
            if(Input.GetKey(left)){
                if(horizontalVel >= -maxHorizontalSpeed){
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * horizontalForce);
                }
            }

            if(Input.GetKey(right)){
                if(horizontalVel <= maxHorizontalSpeed){
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalForce);
                }
            }

            // Slowing down automatically on ground (friction)
            float dragThreshold = 0.5f;
            if(!Input.GetKey(left) && !Input.GetKey(right) && isGrounded){
                if(horizontalVel > dragThreshold){
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * horizontalForce);
                } else if (horizontalVel < -dragThreshold){
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalForce);
                }

                if(Mathf.Abs(horizontalVel) < dragThreshold){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }
    }

    IEnumerator End()
    {
        // float duration = 1.0f;
        // float elapsedTime = 0;
        // while(elapsedTime < duration){
        //     yield return null;
        //     elapsedTime += Time.unscaledDeltaTime;
        //     float ratio = elapsedTime / duration;
        //     Time.timeScale = Mathf.Lerp(1, 0, ratio);
        // }

        // Time.timeScale = 1;
        yield return new WaitForSeconds(1.0f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    bool ignoreNextFlagCollision = false; // The first time you collide with the flag, don't pick it up (since it just spawned)
    void OnTriggerEnter2D(Collider2D col){
        if(col.transform.tag == "Flag"){
            if(ignoreNextFlagCollision){
                ignoreNextFlagCollision = false;
            } else {
                hasFlag = true;
                Destroy(col.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.collider.transform.tag == "Death"){
            if(flagInstance != null){
                ignoreNextFlagCollision = false; // So that you will always pick the flag back up when you respawn at it
                Instantiate(deathEffect, transform.position, transform.rotation);
                transform.position = flagInstance.transform.position;
                GetComponent<Rigidbody2D>().velocity = flagInstance.GetComponent<Rigidbody2D>().velocity;
            } else {
                Instantiate(deathEffect, transform.position, transform.rotation);
                visuals.SetActive(false);
                GetComponent<Rigidbody2D>().simulated = false;
                StartCoroutine(End());
                isDead = true;
            }
        }
    }
}
