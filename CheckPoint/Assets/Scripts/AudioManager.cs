using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    
    public AudioClip doorTriggerSound;
    public AudioClip playerDeathSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
