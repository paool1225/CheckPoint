using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 60; // Time in seconds
    public TMP_Text countdownDisplay;
    public AudioSource audioSource; // AudioSource component for playing sound
    public AudioClip celebrationSound; // Sound to play when countdown ends
    private float currentTime;
    private bool timerIsActive = true;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        if (timerIsActive)
        {
            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerIsActive = false;
                StartCoroutine(EndCountdownRoutine());
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countdownDisplay.text = string.Format("Survive!! Time Remaining: {0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator EndCountdownRoutine()
    {
        countdownDisplay.text = "Congratulations!";
        audioSource.PlayOneShot(celebrationSound); // Play celebration sound
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
    }
}
