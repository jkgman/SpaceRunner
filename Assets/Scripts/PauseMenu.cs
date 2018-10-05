using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Stops the game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Load main menu scene
    /// </summary>
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

        //Application.Quit();
    }
}
