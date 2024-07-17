using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void levelPassed() 
    {   
        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 2) {
            PlayerPrefs.SetInt("Unlocked Level", 3);
            PlayerPrefs.Save();
        }
    }
}
