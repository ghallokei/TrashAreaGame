using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
   private void Start()
    {
        Time.timeScale = 0;
    }

    public void HidePage(GameObject currentPage)
    {
        currentPage.SetActive(false);
    }

    public void ShowPage(GameObject nextPage)
    {
        nextPage.SetActive(true);
    }


    public void LastPage(GameObject lastPage)
    {
        lastPage.SetActive(false);
        Time.timeScale = 1;
    }

}
