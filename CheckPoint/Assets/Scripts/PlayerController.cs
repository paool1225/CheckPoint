using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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

    private bool ignoreNextFlagCollision = false; // To ignore flag collision immediately after throwing

    public AudioSource audioSource, jumpSource, throwSource;  // Separate audio sources for different actions

    private AudioClip deathSound; // Death sound clip

    void Start()
    {
        deathSound = Resources.Load<AudioClip>("Sounds/Death"); // Ensure this path is correct
    }

    void Update()
    {
        if (!isDead)
        {
            flagSprite.SetActive(hasFlag);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = (mousePosition - (Vector2)transform.position).normalized;

            if (Input.GetKey(KeyCode.Mouse0) && hasFlag)
            {
                throwSource.Play();
                hasFlag = false;
                ignoreNextFlagCollision = true;
                flagInstance = Instantiate(flagPrefab, transform.position, Quaternion.identity);
                flagInstance.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (mouseDir * throwVelocity);
            }

            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            headSprite.transform.eulerAngles = new Vector3(0, 0, angle);

            if (Input.GetKey(jump) && isGrounded)
            {
                jumpSource.Play();
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpVelocity);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        PhysicsControls();
    }

    void GroundCheck()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(groundCheck1.transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit2 = Physics2D.Raycast(groundCheck2.transform.position, Vector2.down, 0.1f);
        isGrounded = (hit1.collider != null && hit1.collider.CompareTag("Ground")) || (hit2.collider != null && hit2.collider.CompareTag("Ground"));
    }

    void PhysicsControls()
    {
        float horizontalVel = GetComponent<Rigidbody2D>().velocity.x;
        float horizontalForce = horizontalAccel * GetComponent<Rigidbody2D>().mass;

        if (Input.GetKey(left) && horizontalVel >= -maxHorizontalSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * horizontalForce);

        if (Input.GetKey(right) && horizontalVel <= maxHorizontalSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalForce);
    }

    IEnumerator End()
    {
        Debug.Log("End coroutine started."); // Debug statement

        yield return new WaitForSeconds(1.0f);

        Debug.Log("Reloading scene now."); // Debug statement
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flag"))
        {
            if (!ignoreNextFlagCollision)
            {
                hasFlag = true;
                FlagScript flagScript = col.gameObject.GetComponent<FlagScript>();
                if (flagScript.isStuck)
                    flagScript.ResetFlag();

                Destroy(col.gameObject);
            }
            else
            {
                ignoreNextFlagCollision = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Death"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            PlayDeathSound();

            if (flagInstance != null)
            {
                Debug.Log("Player dying with flag instance available.");
                transform.position = flagInstance.transform.position;
                GetComponent<Rigidbody2D>().velocity = flagInstance.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                Debug.Log("Player dying without flag instance, should reload scene.");
                visuals.SetActive(false);
                GetComponent<Rigidbody2D>().simulated = false;
                StartCoroutine(End());
                isDead = true;
            }
        }
    }

    void PlayDeathSound()
    {
        // Randomly choose a death sound to play from a predefined selection
        int chooseSound = Random.Range(0, 51) % 5; // More sounds can be added and adjusted here
        AudioClip selectedSound = Resources.Load<AudioClip>($"Sounds/death{chooseSound + 1}"); // Assumes sounds are named death1, death2, etc.
        audioSource.PlayOneShot(selectedSound);
        Debug.Log($"Playing death sound: death{chooseSound + 1}");
    }
}
