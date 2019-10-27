using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDropGarbage : MonoBehaviour
{
    private GameObject currentItem;
    public GameObject container;
    public Text ItemHint;
    public Text warning;
    private bool isTouchingItem;
    private bool isTouchingContainer;

    public AudioClip pickUpItemSound;
    
    private void Start()
    {
        isTouchingItem = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isTouchingItem)
        {
            pickUpItemIfPossible();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isTouchingContainer)
        {
            dropItemIfPossible();
        }
    }

    private void dropItemIfPossible()
    {
        Inventory.instance.RemoveGarbage(container.GetComponent<Container>().garbagetype);
        ItemHint.text = "";
    }

    private void pickUpItemIfPossible()
    {
        GarbageItem garbageItem = currentItem.GetComponent<GarbageItem>();

        if (!Inventory.instance.AddGarbage(garbageItem))
        {
            warning.text = "Inventory full";
        }
        else
        {
            warning.text = "";
            ItemHint.text = "";
            Destroy(currentItem);
            GameManager.instance.GetComponent<AudioSource>().clip = pickUpItemSound;
            GameManager.instance.GetComponent<AudioSource>().Play();
            GameManager.instance.garbageList.Remove(currentItem.gameObject);
            GetComponent<Player>().AddPoints(1);
            isTouchingItem = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("garbage"))
        {
            ItemHint.text = "Press F to interact";
            isTouchingItem = true;
            currentItem = other.gameObject;
        }

        if (other.CompareTag("container"))
        {
            ItemHint.text = "Press Space to drop Garbage";
            isTouchingContainer = true;
            container = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("garbage"))
        {
            warning.text = "";
            isTouchingItem = false;
            ItemHint.text = "";
        }

        else if (other.CompareTag("container"))
        {
            isTouchingContainer = false;
            ItemHint.text = "";
        }
    }
}