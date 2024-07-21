using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level8 : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void levelPassed() 
    {   
        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 8) {
            PlayerPrefs.SetInt("Unlocked Level", 9);
            PlayerPrefs.Save();
        }
    }
}
