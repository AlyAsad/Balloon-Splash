using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{

    [SerializeField] private GameObject LevelCompleteUI;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    private bool enemyDead = false;

    private void Update()
    {
        if (enemyDead) return;

        // if enemy died, level passed
        if (enemy == null) {
            enemyDead = true;
            levelPassed();
        }

        // if player died, level failed
        else if (player == null) {
            if (PauseMenu.gameIsPaused == true) return;
            levelFailed();
        }

    }


    public void levelPassed() 
    {   
        LevelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;

        if (PlayerPrefs.GetInt("Unlocked Level", 1) == 1) {
            PlayerPrefs.SetInt("Unlocked Level", 2);
            PlayerPrefs.Save();
        }
    }


    public void levelFailed()
    {
        
    }


}
