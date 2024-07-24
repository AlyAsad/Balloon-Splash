using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionMenu : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    
    public void NextLevel()
    {
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        SceneManager.LoadSceneAsync("Level " + (levelNumber + 1));
    }

    public void Replay()
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
