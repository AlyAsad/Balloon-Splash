using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;
    public GameObject level5Button;
    public GameObject level6Button;
    public GameObject level7Button;




    void Start()
    {   

        // each case statement should fall through to the next one
        switch (PlayerPrefs.GetInt("Unlocked Level", 1)) {

            case 1:
                level2Button.GetComponent<Button>().interactable = false;
                goto case 2;
            
            case 2:
                level3Button.GetComponent<Button>().interactable = false;
                goto case 3;
            
            case 3:
                level4Button.GetComponent<Button>().interactable = false;
                goto case 4;
            
            case 4:
                level5Button.GetComponent<Button>().interactable = false;
                goto case 5;
            
            case 5:
                level6Button.GetComponent<Button>().interactable = false;
                goto case 6;
            
            case 6:
                level7Button.GetComponent<Button>().interactable = false;
                break;
                
            default:
                break;

        }
    }




    public void BackToMenu()
    {
        StartCoroutine(LoadLevelAfterDelay("MainMenu"));
    }

    public void GoToLevel1()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 1"));
    }

    public void GoToLevel2()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 2"));
    }

    public void GoToLevel3()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 3"));
    }

    public void GoToLevel4()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 4"));
    }

    public void GoToLevel5()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 5"));
    }

    public void GoToLevel6()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 6"));
    }

    public void GoToLevel7()
    {
        StartCoroutine(LoadLevelAfterDelay("Level 7"));
    }
    

    IEnumerator LoadLevelAfterDelay(string s) {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(s);
    }
}
