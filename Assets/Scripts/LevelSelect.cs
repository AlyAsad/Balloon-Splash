using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public GameObject level2Button;

    void Start()
    {   

        // each case statement should fall through to the next one
        switch (PlayerPrefs.GetInt("Unlocked Level", 1)) {

            case 1:
                level2Button.GetComponent<Button>().interactable = false;
                goto default;
                
            default:
                break;

        }
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
