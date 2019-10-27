using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    private bool isDisplayed;

    private bool gameIsRunning;

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Tutorial(GameObject tutorialImage)
    {
        if (isDisplayed)
        {
            tutorialImage.SetActive(false);
            isDisplayed = false;
        }
        else
        {
            tutorialImage.SetActive(true);
            isDisplayed = true;
        }
    }

   
}