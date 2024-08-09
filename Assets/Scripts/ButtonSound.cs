using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickSound;
    }

    public void PlaySound()
    {
        DontDestroyOnLoad(gameObject);
        audioSource.time = 0.115f;
        audioSource.Play();
    }
}

