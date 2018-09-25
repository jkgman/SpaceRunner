using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioClip onPress;


    /// <summary>
    /// Load the first scene
    /// </summary>
    public void StartGame()
    {
        SoundManager.Instance.PlaySfx(onPress);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Set volume to used audio sources
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume( float volume)
    {
        SoundManager.Instance.sfxSource.volume = volume;
        SoundManager.Instance.musicSource.volume = volume;
    }

    /// <summary>
    /// Quit the app
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
