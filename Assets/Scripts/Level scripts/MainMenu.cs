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
        if (currLevel > 7) currLevel = 7;

        StartCoroutine(LoadLevelAfterDelay("Level " + currLevel));
    }

    public void LevelSelect()
    {
        StartCoroutine(LoadLevelAfterDelay("Level Select"));
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



    IEnumerator LoadLevelAfterDelay(string s) {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(s);
    }
}

