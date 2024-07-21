using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void levelPassed() 
    {   
        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 4) {
            PlayerPrefs.SetInt("Unlocked Level", 5);
            PlayerPrefs.Save();
        }
    }
}
