using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpUpgrade : MonoBehaviour
{
    public bool upgradeActivated;
    public bool canDestroy;

    public GameObject player;
    public Slider progressSlider;

    public GameObject gun;

    void Start()
    {
        gun.SetActive(false);
        upgradeActivated = false;
        canDestroy = false;
    }

    private void Update()
    {
        if (upgradeActivated)
        {
            StartCoroutine(ActivateUpgrade());
            upgradeActivated = false;
        }

        CheckUpgradeProgress();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canDestroy && other.gameObject.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            GameManager.instance.RemoveEnemy(other.gameObject);
            GetComponent<Player>().AddPoints(50);
        }
    }

    public void CheckUpgradeProgress()
    {
        if (progressSlider.GetComponent<Slider>().value < 109) return;

        upgradeActivated = true;
        progressSlider.GetComponent<Slider>().value = 0;
    }

    IEnumerator ActivateUpgrade()
    {
        gun.SetActive(true);
        player.tag = "Untagged";
        canDestroy = true;
        yield return new WaitForSeconds(5);
        canDestroy = false;
        gun.SetActive(false);
        player.tag = "player";
    }
}