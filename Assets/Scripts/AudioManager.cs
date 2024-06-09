using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource mainMenuSound;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void PlayCoinSound()
    {
        coinSound.Play();
    }

    public void PlayMainMenuSound()
    {
        mainMenuSound.Play();
    }
}
