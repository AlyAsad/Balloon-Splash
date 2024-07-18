using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int currLevel;

    public void PlayGame()
    {
        currLevel = PlayerPrefs.GetInt("Unlocked Level", 1);
        switch (currLevel) {

            case 1:
                SceneManager.LoadSceneAsync("Level 1");
                break;
            
            case 2:
                SceneManager.LoadSceneAsync("Level 2");
                break;
            
            default:
                SceneManager.LoadSceneAsync("Level " + (currLevel - 1));
                break;

        }
    }

    public void LevelSelect()
    {
        SceneManager.LoadSceneAsync("Level Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetLevelProgress()
    {
        PlayerPrefs.SetInt("Unlocked Level", 1);
        PlayerPrefs.Save();
    }

    public void GoToSandbox()
    {
        SceneManager.LoadSceneAsync("Level Design");
    }
}

