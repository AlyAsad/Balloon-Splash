using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedMenu : MonoBehaviour
{
    [SerializeField] private int levelNumber;

    public void Retry()
    {
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        SceneManager.LoadSceneAsync("Level " + levelNumber);
    }


    public void BackToMenu()
    {
        
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
