using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Basic menu behaviour
/// </summary>
public class MainMenu : MonoBehaviour {


    /// <summary>
    /// Loads the first scene
    ///
    /// </summary>
    public void StartGame()
    {
        // Change to point to the specific level
        GameManager.Instance.currentLevel = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Mutes music audio source
    /// </summary>
    /// <param name="volume"></param>
    public void MuteMusic()
    {
        SoundManager.Instance.musicSource.mute = !SoundManager.Instance.musicSource.mute;
    }

    /// <summary>
    /// Mutes sound effect audio source
    /// </summary>
    public void MuteSfx()
    {
        SoundManager.Instance.sfxSource.mute = !SoundManager.Instance.sfxSource.mute;
        
    }

    /// <summary>
    /// Quit the app
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
