using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultPlayerController : MonoBehaviour
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

    // Audio management
    public AudioSource audioSource;

    private AudioClip throwSound, jumpSound;
    public AudioClip deathSound;

    void Start()
    { 
        throwSound = Resources.Load<AudioClip>("Sounds/Throw");
        jumpSound = Resources.Load<AudioClip>("Sounds/jump1");
    }

    void Update()
    {
        if (!isDead)
        {
            // Flag visual
            flagSprite.SetActive(hasFlag);

            // Flag throwing
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = (mousePosition - (Vector2)transform.position).normalized;
            if (Input.GetKey(KeyCode.Mouse0) && hasFlag)
            {
                audioSource.PlayOneShot(throwSound);
                hasFlag = false;
                flagInstance = Instantiate(flagPrefab, transform.position, Quaternion.identity);
                flagInstance.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (mouseDir * throwVelocity);
            }

            // Looking direction
            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            headSprite.transform.eulerAngles = new Vector3(0, 0, angle);

            // Jumping
            if (Input.GetKey(jump) && isGrounded)
            {
                audioSource.PlayOneShot(jumpSound);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpVelocity);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            GroundCheck();
            PhysicsControls();
        }
    }

    void GroundCheck()
    {
        isGrounded = false;
        RaycastHit2D hit1 = Physics2D.Raycast(groundCheck1.transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit2 = Physics2D.Raycast(groundCheck2.transform.position, Vector2.down, 0.1f);
        isGrounded = (hit1.collider != null && hit1.collider.CompareTag("Ground")) ||
                     (hit2.collider != null && hit2.collider.CompareTag("Ground"));
    }

    void PhysicsControls()
    {
        float horizontalVel = GetComponent<Rigidbody2D>().velocity.x;
        float horizontalForce = horizontalAccel * GetComponent<Rigidbody2D>().mass;

        if (Input.GetKey(left) && horizontalVel > -maxHorizontalSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * horizontalForce);
        if (Input.GetKey(right) && horizontalVel < maxHorizontalSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalForce);
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flag") && !hasFlag)
        {
            hasFlag = true;
            Destroy(col.gameObject); // Destroys the flag object
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Death"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(deathSound);
            if (flagInstance != null)
            {
                transform.position = flagInstance.transform.position;
                GetComponent<Rigidbody2D>().velocity = flagInstance.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                visuals.SetActive(false);
                GetComponent<Rigidbody2D>().simulated = false;
                StartCoroutine(End());
                isDead = true;
            }
        }
    }
}
