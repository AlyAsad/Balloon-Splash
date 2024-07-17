using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    void Start()
    {
        //if (PlayerPrefs.GetInt("Unlocked Level", 1) == 1)
    }




    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }
}
