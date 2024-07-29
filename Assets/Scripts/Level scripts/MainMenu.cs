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
        if (currLevel > 5) currLevel = 5;

        SceneManager.LoadSceneAsync("Level " + currLevel);
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

    public void GoToAffan()
    {
        SceneManager.LoadSceneAsync("Affan");
    }

    public void GoToEbad()
    {
        SceneManager.LoadSceneAsync("Ebad");
    }
}

