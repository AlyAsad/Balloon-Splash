using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5 : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void levelPassed() 
    {   
        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 5) {
            PlayerPrefs.SetInt("Unlocked Level", 6);
            PlayerPrefs.Save();
        }
    }
}