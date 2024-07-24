using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool gameIsPaused = false;

    [SerializeField] private int levelNumber;
    [SerializeField] private GameObject pauseMenuUI;
    

    public void Pause()
    {
        if (gameIsPaused) return;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        if (!gameIsPaused) return;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadSceneAsync("Level " + levelNumber);
    }


    public void BackToMenu()
    {
        
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
}
