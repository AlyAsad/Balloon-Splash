using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{

    public void levelPassed() 
    {   
        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 1) {
            PlayerPrefs.SetInt("Unlocked Level", 2);
            PlayerPrefs.Save();
        }
    }

}
