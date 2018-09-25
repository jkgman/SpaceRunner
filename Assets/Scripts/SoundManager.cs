using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    // Initialize singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    /// Plays the parameter clip once
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();

    }

    /// <summary>
    /// Loops the clip sent as parameter
    /// </summary>
    /// <param name="musicClip"></param>
    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;

        musicSource.Play();
        musicSource.loop = true;
    }

}
