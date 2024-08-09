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
        StartCoroutine(LoadLevelAfterDelay("Level " + (levelNumber + 1)));
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        StartCoroutine(LoadLevelAfterDelay("Level " + levelNumber));
    }


    public void BackToMenu()
    {
        
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        StartCoroutine(LoadLevelAfterDelay("MainMenu"));
    }

    IEnumerator LoadLevelAfterDelay(string s) {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(s);
    }
}
