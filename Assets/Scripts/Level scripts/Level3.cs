using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{

    [SerializeField] private GameObject LevelCompleteUI;
    [SerializeField] private GameObject LevelFailedUI;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    private bool lvlPassed = false, lvlFailed = false;


    private void Update()
    {
        if (lvlPassed || lvlFailed) return;

        // if enemy died, level passed
        if (enemy == null) {
            lvlPassed = true;
            levelPassed();
        }

        // if player died, level failed
        else if (player == null) {
            lvlFailed = true;
            levelFailed();
        }

    }


    public void levelPassed() 
    {   
        LevelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;

        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 3) {
            PlayerPrefs.SetInt("Unlocked Level", 4);
            PlayerPrefs.Save();
        }
    }


    public void levelFailed()
    {
        LevelFailedUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;
    }


}
